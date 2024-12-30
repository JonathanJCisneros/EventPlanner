using EventPlanner.Core;
using EventPlanner.Core.Extension_Methods;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Repository.Query_Builder;
using System.Data;

namespace EventPlanner.Repository
{
    public class InquiryRepository : IInquiryRepository
    {
        #region Fields

        private readonly IMySqlRepository db;

        #endregion Fields

        #region Constructors

        public InquiryRepository(IMySqlRepository mySqlRepository)
        {
            db = mySqlRepository;
        }

        #endregion Constructors

        #region Private Methods

        private static Dictionary<string, object?> BuildParameters(Inquiry inquiry)
        {
            return new()
            {
                { "@id", inquiry.Id },
                { "@name", inquiry.Name },
                { "@email", inquiry.Email },
                { "@subject", inquiry.Subject.GetDisplayName() },
                { "@status", inquiry.Status.GetDisplayName() },
                { "@message", inquiry.Message },
                { "@created_date", inquiry.CreatedDate },
                { "@updated_date", inquiry.UpdatedDate }
            };
        }

        private static List<Inquiry> ConvertTable(DataTable dt)
        {
            return [.. dt.AsEnumerable().Select(x => new Inquiry
            {
                Id = x.Field<Guid>("id"),
                Name = x.Field<string>("name"),
                Email = x.Field<string>("email"),
                Subject = EnumExtensions.GetValueFromName<InquirySubject>(x.Field<string>("subject")),
                Status = EnumExtensions.GetValueFromName<InquiryStatus>(x.Field<string>("status")),
                Message = x.Field<string>("message"),
                CreatedDate = x.Field<DateTime>("created_date"),
                UpdatedDate = x.Field<DateTime>("updated_date")
            })];
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<Inquiry?> Get(Guid id)
        {
            string query = @"SELECT * 
                             FROM inquiries 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return dt.Rows.Count == 0 ? null : ConvertTable(dt)[0];
        }

        public async Task<List<Inquiry>> GetMany(List<Guid> ids)
        {
            if (ids.Count == 0)
            {
                return [];
            }

            string query = @"SELECT *
                             FROM inquiries";

            QueryBuilderResponse response = QueryBuilder.WhereQueryOneKey(ids.Select(x => x.ToString()).ToList(), "id", "OR");

            query += @$"{response.Query} 
                        ORDER BY created_date DESC;";

            DataTable dt = await db.ExecuteQuery(query, response.Parameters);

            return ConvertTable(dt);
        }

        public async Task<List<Inquiry>> GetAll()
        {
            string query = "SELECT * FROM inquiries;";

            DataTable dt = await db.ExecuteQuery(query, []);

            return ConvertTable(dt);
        }

        public async Task<bool> Create(Inquiry inquiry)
        {
            string query = @"INSERT INTO inquiries
                                         (
                                             id,
                                             name,
                                             email,
                                             subject,
                                             status,
                                             message,
                                             created_date,
                                             updated_date
                                         )
                                         VALUES
                                         (
                                             @id,
                                             @name,
                                             @email,
                                             @subject,
                                             @status,
                                             @message,
                                             @created_date,
                                             @updated_date
                                         );";

            return await db.ExecuteNonQuery(query, BuildParameters(inquiry));
        }

        public async Task<bool> Update(Inquiry inquiry)
        {
            string query = @"UPDATE inquiries
                             SET name = @name,
                                 email = @email,
                                 subject = @subject,
                                 status = @status,
                                 message = @message,
                                 created_date = @created_date,
                                 updated_date = @updated_date
                             WHERE id = @id;";

            return await db.ExecuteNonQuery(query, BuildParameters(inquiry));
        }

        public async Task<bool> Delete(Guid id)
        {
            string query = @"DELETE FROM inquiries 
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