using EventPlanner.Core.Address;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Repository.Query_Builder;
using System.Data;

namespace EventPlanner.Repository
{
    public class AddressRepository : IAddressRepository
    {
        #region Fields

        private readonly IMySqlRepository db;

        #endregion Fields

        #region Constructors

        public AddressRepository(IMySqlRepository mySqlRepository)
        {
            db = mySqlRepository;
        }

        #endregion Constructors

        #region Private Methods

        private static Dictionary<string, object?> BuildParameters(Address address)
        {
            return new()
            {
                { "@id", address.Id },
                { "@name", address.Name },
                { "@street_line_one", address.StreetLineOne },
                { "@street_line_two", address.StreetLineTwo },
                { "@street_line_three", address.StreetLineThree },
                { "@city", address.City },
                { "@state", address.State },
                { "@postal_code", address.PostalCode },
                { "@country", address.Country },
                { "@longitude", address.Coordinates.Longitude },
                { "@latitude", address.Coordinates.Latitude },
                { "@phone_number", address.PhoneNumber },
                { "@email", address.Email },
                { "@created_date", address.CreatedDate },
                { "@updated_date", address.UpdatedDate }
            };
        }

        private static List<Address> ConvertTable(DataTable dt)
        {
            return [.. dt.AsEnumerable().Select(x => new Address
            {
                Id = x.Field<Guid>("id"),
                Name = x.Field<string>("name"),
                StreetLineOne = x.Field<string>("street_line_one"),
                StreetLineTwo = x.Field<string?>("street_line_two"),
                StreetLineThree = x.Field<string?>("street_line_three"),
                City = x.Field<string>("city"),
                State = x.Field<string>("state"),
                PostalCode = x.Field<string>("postal_code"),
                Country = x.Field<string>("country"),
                Coordinates = new()
                {
                    Longitude = x.Field<decimal>("longitude"),
                    Latitude = x.Field<decimal>("latitude")
                },
                Email = x.Field<string>("email"),
                PhoneNumber = x.Field<string>("phone_number"),
                CreatedDate = x.Field<DateTime>("created_date"),
                UpdatedDate = x.Field<DateTime>("updated_date")
            })];
        }

        #endregion Private Methods

        #region Public Methods

        public async Task<Address?> Get(Guid id)
        {
            string query = @"SELECT * 
                             FROM addresses 
                             WHERE id = @id;";

            Dictionary<string, object?> parameters = new()
            {
                { "@id", id }
            };

            DataTable dt = await db.ExecuteQuery(query, parameters);

            return dt.Rows.Count == 0 ? null : ConvertTable(dt)[0];
        }

        public async Task<List<Address>> GetMany(List<Guid> ids)
        {
            if (ids.Count == 0)
            {
                return [];
            }

            string query = @"SELECT *
                             FROM addresses";

            QueryBuilderResponse response = QueryBuilder.WhereQueryOneKey(ids.Select(x => x.ToString()).ToList(), "id", "OR");

            query += @$"{response.Query} 
                        ORDER BY created_date DESC;";

            DataTable dt = await db.ExecuteQuery(query, response.Parameters);

            return ConvertTable(dt);
        }

        public async Task<List<Address>> GetAll()
        {
            string query = "SELECT * FROM addresses;";

            DataTable dt = await db.ExecuteQuery(query, []);

            return ConvertTable(dt);
        }

        public async Task<bool> Create(Address address)
        {
            string query = @"INSERT INTO addresses
                                         (
                                             id, 
                                             name, 
                                             street_line_one, 
                                             street_line_two, 
                                             street_line_three,
                                             city, 
                                             state,
                                             postal_code, 
                                             country, 
                                             longitude, 
                                             latitude, 
                                             phone_number, 
                                             email,
                                             created_date, 
                                             updated_date
                                         ) 
                                         VALUES
                                         (
                                             @id, 
                                             @name, 
                                             @street_line_one, 
                                             @street_line_two, 
                                             @street_line_three,
                                             @city, 
                                             @state,
                                             @postal_code, 
                                             @country, 
                                             @longitude, 
                                             @latitude, 
                                             @phone_number, 
                                             @email, 
                                             @created_date, 
                                             @updated_date
                                         );";

            return await db.ExecuteNonQuery(query, BuildParameters(address));
        }

        public async Task<bool> Update(Address address)
        {
            string query = @"UPDATE addresses 
                             SET name = @name, 
                                 street_line_one = @street_line_one, 
                                 street_line_two = @street_line_two, 
                                 street_line_three = @street_line_three, 
                                 city = @city,
                                 state = @state, 
                                 postal_code = @postal_code, 
                                 country = @country, 
                                 longitude = @longitude, 
                                 latitude = @latitude, 
                                 phone_number = @phone_number,
                                 email = @email,
                                 created_date = created_date, 
                                 updated_date = @updated_date 
                             WHERE id = @id;";

            return await db.ExecuteNonQuery(query, BuildParameters(address));
        }

        public async Task<bool> Delete(Guid id)
        {
            string query = @"DELETE FROM addresses 
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
