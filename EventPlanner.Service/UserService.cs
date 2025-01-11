using EventPlanner.Core.User;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Service.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace EventPlanner.Service
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields        

        #region Constructors

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Constructors

        #region Private Methods

        private const int KeySize = 64;
        private const int Iterations = 350000;
        private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        private string HashPasword(string password, out string salt)
        {
            byte[] saltArray = RandomNumberGenerator.GetBytes(KeySize);

            salt = Encoding.UTF8.GetString(saltArray, 0, saltArray.Length);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltArray,
                Iterations,
                Algorithm,
                KeySize
            );

            return Convert.ToHexString(hash);
        }

        private bool VerifyPassword(string password, string hash, byte[] salt)
        {
            byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password, 
                salt, 
                Iterations, 
                Algorithm, 
                KeySize
            );

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<User?> Get(Guid id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<bool> UserExists(string email)
        {
            User? user = await _userRepository.CheckByEmail(email);

            return user != null;
        }

        public async Task<List<User>> GetMany(List<Guid> ids)
        {
            return await _userRepository.GetMany(ids);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<AuthorizeResult> Authorize(string email, string password)
        {
            AuthorizeResult response = new();

            User? user = await _userRepository.CheckByEmail(email);

            if (user == null)
            {
                response.Message = "We're sorry, user does not match our records.";

                return response;
            }

            response = new(user);

            if (response.LockedOut) 
            {
                response.Message = "Your account is currently locked out, please reset your password.";

                return response;
            } 

            if (!VerifyPassword(password, user.Password, Encoding.UTF8.GetBytes(user.Salt)))
            {
                response.PasswordMatched = false;
                response.Message = "We're sorry, we are unable to find an account matching this email and/or password.";

                user.PasswordAttempts++;
                user.UpdatedDate = DateTime.UtcNow;

                if (user.PasswordAttempts == 3)
                {
                    user.IsAuthorized = false;
                    response.LockedOut = true;
                }

                _ = await _userRepository.Update(user);

                return response;
            }
            
            if (user.PasswordAttempts > 0)
            {
                user.UpdatedDate = DateTime.UtcNow;
                user.PasswordAttempts = 0;
                user.IsAuthorized = true;

                _ = await _userRepository.Update(user);
            }

            await _userRepository.Login(user.Id);

            return response;
        }

        public async Task Logout(Guid id)
        {
            await _userRepository.Logout(id);
        } 

        public async Task<bool> Create(User user)
        {
            user.Password = HashPasword(user.Password, out string salt);
            user.Salt = salt;

            return await _userRepository.Create(user);
        }

        public async Task<bool> Update(User user)
        {
            User? dbUser = await Get(user.Id);

            if (dbUser == null)
            {
                return false;
            }

            user.Password = dbUser.Password;
            user.Salt = dbUser.Salt;
            user.IsAuthorized = dbUser.IsAuthorized;
            user.IsActive = dbUser.IsActive;
            user.PasswordAttempts = dbUser.PasswordAttempts;
            user.LastLoggedIn = dbUser.LastLoggedIn;
            user.LastLoggedOut = dbUser.LastLoggedOut;
            user.UpdatedDate = DateTime.UtcNow;

            return await _userRepository.Update(user);
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _userRepository.Delete(id);
        }     

        #endregion Public Methods
    }
}
