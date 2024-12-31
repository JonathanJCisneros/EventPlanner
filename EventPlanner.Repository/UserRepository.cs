using EventPlanner.Core.User;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Repository.Query_Builder;
using System.Data;

namespace EventPlanner.Repository
{
    public class UserRepository : IUserRepository
    {
        #region Fields

        private readonly IMySqlRepository db;

        #endregion Fields

        #region Constructors

        public UserRepository(IMySqlRepository mySqlRepository)
        {
            db = mySqlRepository;
        }

        #endregion Constructors

        #region Private Methods

        private static Dictionary<string, object?> BuildParameters(User user)
        {
            return new()
            {
                { "@id", user.Id },
                { "@first_name", user.FirstName },
                { "@last_name", user.LastName },
                { "@email", user.Email },
                { "@phone_number", user.PhoneNumber },
                { "@password", user.Password },
                { "@salt", user.Salt },
                { "@is_authorized", user.IsAuthorized },
                { "@is_active", user.IsActive },
                { "@password_attempts", user.PasswordAttempts },
                { "@last_logged_in", user.LastLoggedIn },
                { "@last_logged_out", user.LastLoggedOut },
                { "@created_date", user.CreatedDate },
                { "@updated_date", user.UpdatedDate }
            };
        }

        private static List<User> ConvertTable(DataTable dt)
        {
            return [.. dt.AsEnumerable().Select(x => new User
            {
                Id = x.Field<Guid>("id"),
                FirstName = x.Field<string>("first_name"),
                LastName = x.Field<string>("last_name"),
                Email = x.Field<string>("email"),
                PhoneNumber = x.Field<string>("phone_number"),
                Password = x.Field<string>("password"),
                Salt = x.Field<string>("salt"),
                Roles = [],
                IsAuthorized = x.Field<sbyte>("is_authorized") == 1,
                IsActive = x.Field<sbyte>("is_active") == 1,
                PasswordAttempts = x.Field<int>("password_attempts"),
                LastLoggedIn = x.Field<DateTime>("last_logged_in"),
                LastLoggedOut = x.Field<DateTime?>("last_logged_out"),
                CreatedDate = x.Field<DateTime>("created_date"),
                UpdatedDate = x.Field<DateTime>("updated_date")
            })];
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<User?> CheckByEmail(string email)
        {
            string query = @"SELECT * 
                             FROM users 
                             WHERE email = @email;";

            Dictionary<string, object?> parameters = new()
            {
                { "@email", email }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return dt.Rows.Count == 0 ? null : ConvertTable(dt)[0];
        }

        public async Task<User?> Get(Guid id)
        {
            string query = @"SELECT * 
                             FROM users 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return dt.Rows.Count == 0 ? null : ConvertTable(dt)[0];
        }        

        public async Task<List<User>> GetMany(List<Guid> ids)
        {
            if (ids.Count == 0)
            {
                return [];
            }

            string query = @"SELECT *
                             FROM users";

            QueryBuilderResponse response = QueryBuilder.WhereQueryOneKey(ids.Select(x => x.ToString()).ToList(), "id", "OR");

            query += @$"{response.Query} 
                        ORDER BY created_date DESC;";

            DataTable dt = await db.ExecuteQuery(query, response.Parameters);

            return ConvertTable(dt);
        }

        public async Task<List<User>> GetAll()
        {
            string query = "SELECT * FROM users;";

            DataTable dt = await db.ExecuteQuery(query, []);

            return ConvertTable(dt);
        }

        public async Task<bool> Create(User user)
        {
            string query = @"INSERT INTO users
                                         (
                                             id, 
                                             first_name, 
                                             last_name, 
                                             email, 
                                             phone_number,
                                             password, 
                                             salt,
                                             is_authorized, 
                                             is_active, 
                                             password_attempts, 
                                             last_logged_in, 
                                             last_logged_out, 
                                             created_date, 
                                             updated_date
                                         ) 
                                         VALUES
                                         (
                                             @id, 
                                             @first_name, 
                                             @last_name, 
                                             @email, 
                                             @phone_number,
                                             @password, 
                                             @salt, 
                                             @is_authorized, 
                                             @is_active, 
                                             @password_attempts, 
                                             @last_logged_in, 
                                             @last_logged_out, 
                                             @created_date, 
                                             @updated_date
                                         );";

            return await db.ExecuteNonQuery(query, BuildParameters(user));
        }

        public async Task Login(Guid id)
        {
            string query = @"UPDATE users 
                             SET last_logged_in = @time, updated_date = @time 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id },
                { "@time", DateTime.UtcNow }
            };

            await db.ExecuteNonQuery(query, parameters);
        }

        public async Task Logout(Guid id)
        {
            string query = @"UPDATE users 
                             SET last_logged_out = @time, updated_date = @time 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id },
                { "@time", DateTime.UtcNow }
            };

            await db.ExecuteNonQuery(query, parameters);
        }

        public async Task<bool> Update(User user)
        {
            string query = @"UPDATE users 
                             SET first_name = @first_name, 
                                 last_name = @last_name, 
                                 email = @email, 
                                 password = @password, 
                                 salt = @salt,
                                 is_authorized = @is_authorized, 
                                 is_active = @is_active, 
                                 password_attempts = @password_attempts, 
                                 last_logged_in = @last_logged_in, 
                                 last_logged_out = @last_logged_out, 
                                 created_date = created_date, 
                                 updated_date = @updated_date 
                             WHERE id = @id;";

            return await db.ExecuteNonQuery(query, BuildParameters(user));
        }

        public async Task<bool> Delete(Guid id)
        {
            string query = @"DELETE FROM users 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            return await db.ExecuteNonQuery(query, parameters);
        }

        #endregion Public Methods
    }
}