using EventPlanner.Core.Notification;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Repository.Query_Builder;
using System.Data;

namespace EventPlanner.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        #region Fields

        private readonly IMySqlRepository db;

        #endregion Fields

        #region Constructors

        public NotificationRepository(IMySqlRepository mySqlRepository)
        {
            db = mySqlRepository;
        }

        #endregion Constructors

        #region Private Methods

        private static Dictionary<string, object?> BuildParameters(Notification notification)
        {
            return new()
            {
                { "@id", notification.Id },
                { "@title", notification.Title },
                { "@subject", notification.Subject },
                { "@description", notification.Description },
                { "@event_id", notification.EventId },
                { "@author_id", notification.AuthorId },
                { "@created_date", notification.CreatedDate },
                { "@updated_date", notification.UpdatedDate }
            };
        }

        private static List<Notification> ConvertTable(DataTable dt)
        {
            return [.. dt.AsEnumerable().Select(x => new Notification
            {
                Id = x.Field<Guid>("id"),
                Title = x.Field<string>("title"),
                Subject = x.Field<string?>("subject"),
                Description = x.Field<string>("description"),
                EventId = x.Field<Guid>("event_id"),
                AuthorId = x.Field<Guid>("author_id"),
                CreatedDate = x.Field<DateTime>("created_date"),
                UpdatedDate = x.Field<DateTime>("updated_date")
            })];
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<Notification?> Get(Guid id)
        {
            string query = @"SELECT * 
                             FROM notifications 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return dt.Rows.Count == 0 ? null : ConvertTable(dt)[0];
        }

        public async Task<List<Notification>> GetMany(List<Guid> ids)
        {
            if (ids.Count == 0)
            {
                return [];
            }

            string query = @"SELECT *
                             FROM notifications";

            QueryBuilderResponse response = QueryBuilder.WhereQueryOneKey(ids.Select(x => x.ToString()).ToList(), "id", "OR");

            query += @$"{response.Query} 
                        ORDER BY created_date DESC;";

            DataTable dt = await db.ExecuteQuery(query, response.Parameters);

            return ConvertTable(dt);
        }

        public async Task<List<Notification>> GetUnreadForUser(Guid id)
        {
            string query = @"SELECT n.*
                             FROM recipients r
                             LEFT JOIN notifications n 
                             ON n.id = r.notification_id
                             WHERE r.user_id = @id AND r.status = 'Unread';";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return ConvertTable(dt);
        }

        public async Task<List<Notification>> GetAll()
        {
            string query = "SELECT * FROM notifications;";

            DataTable dt = await db.ExecuteQuery(query, []);

            return ConvertTable(dt);
        }

        public async Task<bool> Create(Notification notification)
        {
            string query = @"INSERT INTO notifications
                                         (
                                             id, 
                                             title, 
                                             subject, 
                                             description, 
                                             event_id,
                                             author_id, 
                                             created_date, 
                                             updated_date
                                         ) 
                                         VALUES
                                         (
                                             @id, 
                                             @title, 
                                             @subject, 
                                             @description, 
                                             @event_id,
                                             @author_id, 
                                             @created_date, 
                                             @updated_date
                                         );";

            return await db.ExecuteNonQuery(query, BuildParameters(notification));
        }

        public async Task<bool> Update(Notification notification)
        {
            string query = @"UPDATE notifications 
                             SET title = @title, 
                                 subject = @subject, 
                                 description = @description, 
                                 event_id = @event_id, 
                                 author_id = @author_id, 
                                 created_date = created_date, 
                                 updated_date = @updated_date 
                             WHERE id = @id;";

            return await db.ExecuteNonQuery(query, BuildParameters(notification));
        }

        public async Task<bool> Delete(Guid id)
        {
            string query = @"DELETE FROM notifications 
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