using EventPlanner.Core.Extension_Methods;
using EventPlanner.Core.Notification;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Repository.Query_Builder;
using System.Data;

namespace EventPlanner.Repository
{
    public class RecipientRepository : IRecipientRepository
    {
        #region Fields

        private readonly IMySqlRepository db;

        #endregion Fields

        #region Constructors

        public RecipientRepository(IMySqlRepository mySqlRepository)
        {
            db = mySqlRepository;
        }

        #endregion Constructors

        #region Private Methods

        private static Dictionary<string, object?> BuildParameters(Recipient recipient)
        {
            return new()
            {
                { "@id", recipient.Id },
                { "@status", recipient.Status.GetDisplayName() },
                { "@notification_id", recipient.NotificationId },
                { "@user_id", recipient.UserId },
                { "@created_date", recipient.CreatedDate },
                { "@updated_date", recipient.UpdatedDate }
            };
        }

        private static List<Recipient> ConvertTable(DataTable dt)
        {
            return [.. dt.AsEnumerable().Select(x => new Recipient
            {
                Id = x.Field<Guid>("id"),
                Status = EnumExtensions.GetValueFromName<NotificiationStatus>(x.Field<string>("status")),
                NotificationId = x.Field<Guid>("notification_id"),
                UserId = x.Field<Guid>("user_id"),
                CreatedDate = x.Field<DateTime>("created_date"),
                UpdatedDate = x.Field<DateTime>("updated_date")
            })];
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<Recipient?> Get(Guid id)
        {
            string query = @"SELECT * 
                             FROM recipients 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return dt.Rows.Count == 0 ? null : ConvertTable(dt)[0];
        }

        public async Task<List<Recipient>> GetMany(List<Guid> ids)
        {
            if (ids.Count == 0)
            {
                return [];
            }

            string query = @"SELECT *
                             FROM recipients";

            QueryBuilderResponse response = QueryBuilder.WhereQueryOneKey(ids.Select(x => x.ToString()).ToList(), "id", "OR");

            query += @$"{response.Query} 
                        ORDER BY created_date DESC;";

            DataTable dt = await db.ExecuteQuery(query, response.Parameters);

            return ConvertTable(dt);
        }

        public async Task<List<Recipient>> GetAll()
        {
            string query = "SELECT * FROM recipients;";

            DataTable dt = await db.ExecuteQuery(query, []);

            return ConvertTable(dt);
        }

        public async Task<bool> Create(Recipient recipient)
        {
            string query = @"INSERT INTO recipients
                                         (
                                             id, 
                                             status, 
                                             notification_id, 
                                             user_id, 
                                             created_date, 
                                             updated_date
                                         ) 
                                         VALUES
                                         (
                                             @id, 
                                             @status, 
                                             @notification_id, 
                                             @user_id, 
                                             @created_date, 
                                             @updated_date
                                         );";

            return await db.ExecuteNonQuery(query, BuildParameters(recipient));
        }

        public async Task<bool> Update(Recipient recipient)
        {
            string query = @"UPDATE recipients 
                             SET status = @status, 
                                 notification_id = @notification_id, 
                                 user_id = @user_id,
                                 created_date = created_date, 
                                 updated_date = @updated_date 
                             WHERE id = @id;";

            return await db.ExecuteNonQuery(query, BuildParameters(recipient));
        }

        public async Task<bool> Delete(Guid id)
        {
            string query = @"DELETE FROM recipients 
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
