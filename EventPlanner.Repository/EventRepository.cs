using EventPlanner.Core.Event;
using EventPlanner.Core.Extension_Methods;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Repository.Query_Builder;
using System.Data;

namespace EventPlanner.Repository
{
    public class EventRepository : IEventRepository
    {
        #region Fields

        private readonly IMySqlRepository db;

        #endregion Fields

        #region Constructors

        public EventRepository(IMySqlRepository mySqlRepository)
        {
            db = mySqlRepository;
        }

        #endregion Constructors

        #region Private Methods

        private static Dictionary<string, object?> BuildParameters(Event entity)
        {
            return new()
            {
                { "@id", entity.Id },
                { "@name", entity.Name },
                { "@type", EnumExtensions.GetDisplayName(entity.Type) },
                { "@description", entity.Description },
                { "@guest_max", entity.GuestMax },
                { "@start_date", entity.StartDate },
                { "@end_date", entity.EndDate },
                { "@address_id", entity.AddressId },
                { "@created_date", entity.CreatedDate },
                { "@updated_date", entity.UpdatedDate }
            };
        }

        private static List<Event> ConvertTable(DataTable dt)
        {
            return [.. dt.AsEnumerable().Select(x => new Event
            {
                Id = x.Field<Guid>("id"),
                Name = x.Field<string>("name"),
                Type = EnumExtensions.GetValueFromName<EventType>(x.Field<string>("last_name")),
                Description = x.Field<string>("description"),
                GuestMax = x.Field<int>("guest_max"),
                StartDate = x.Field<DateTime>("start_date"),
                EndDate = x.Field<DateTime>("end_date"),
                AddressId = x.Field<Guid>("address_id"),
                CreatedDate = x.Field<DateTime>("created_date"),
                UpdatedDate = x.Field<DateTime>("updated_date")
            })];
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<Event?> Get(Guid id)
        {
            string query = @"SELECT * 
                             FROM events 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return dt.Rows.Count == 0 ? null : ConvertTable(dt)[0];
        }

        public async Task<List<Event>> GetMany(List<Guid> ids)
        {
            if (ids.Count == 0)
            {
                return [];
            }

            string query = @"SELECT *
                             FROM events";

            QueryBuilderResponse response = QueryBuilder.WhereQueryOneKey(ids.Select(x => x.ToString()).ToList(), "id", "OR");

            query += @$"{response.Query} 
                        ORDER BY created_date DESC;";

            DataTable dt = await db.ExecuteQuery(query, response.Parameters);

            return ConvertTable(dt);
        }

        public async Task<List<Event>> GetAll()
        {
            string query = "SELECT * FROM events;";

            DataTable dt = await db.ExecuteQuery(query, []);

            return ConvertTable(dt);
        }

        public async Task<bool> Create(Event entity)
        {
            string query = @"INSERT INTO events
                                         (
                                             id, 
                                             name, 
                                             type, 
                                             description, 
                                             guest_max, 
                                             start_date,
                                             end_date, 
                                             address_id,
                                             created_date, 
                                             updated_date
                                         ) 
                                         VALUES
                                         (
                                             @id, 
                                             @name, 
                                             @type, 
                                             @description, 
                                             @guest_max, 
                                             @start_date,
                                             @end_date, 
                                             @address_id,
                                             @created_date, 
                                             @updated_date
                                         );";

            return await db.ExecuteNonQuery(query, BuildParameters(entity));
        }

        public async Task<bool> Update(Event entity)
        {
            string query = @"UPDATE events 
                             SET name = @name, 
                                 type = @type, 
                                 description = @description, 
                                 guest_max = @guest_max, 
                                 start_date = @start_date,
                                 end_date = @end_date, 
                                 address_id = @address_id, 
                                 created_date = created_date, 
                                 updated_date = @updated_date 
                             WHERE id = @id;";

            return await db.ExecuteNonQuery(query, BuildParameters(entity));
        }

        public async Task<bool> Delete(Guid id)
        {
            string query = @"DELETE FROM events 
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
