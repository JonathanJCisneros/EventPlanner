using EventPlanner.Server.Authorization;
using EventPlanner.Server.Models;
using EventPlanner.Server.Models.Authentication;
using EventPlanner.Server.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventPlanner.Server.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly AppSettings appSettings;

        #endregion Fields        

        #region Constructors

        public UserService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }

        #endregion Constructors

        #region Private Methods

        private readonly List<User> users = new()
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Test",
                LastName = "User",
                Email = "test@email.com",
                Password = "testing1234"
            }
        };

        private string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(this.appSettings.Secret);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity([new Claim("id", user.Id.ToString())]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion Private Methods

        #region Public Methods

        public AuthenticationResponse? Authenticate(AuthenticationRequest model)
        {
            User? user = users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null)
            {
                return null;
            }

            string token = GenerateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return this.users;
        }

        public User? GetById(Guid id)
        {
            return this.users.FirstOrDefault(x => x.Id == id);
        }

        #endregion Public Methods
    }
}
