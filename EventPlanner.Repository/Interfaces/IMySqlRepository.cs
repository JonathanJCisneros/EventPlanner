using System.Data;

namespace EventPlanner.Repository.Interfaces
{
    public interface IMySqlRepository
    {
        Task<bool> ExecuteNonQuery(string query, Dictionary<string, object?> parameters);

        Task<DataTable> ExecuteQuery(string query, Dictionary<string, object?> parameters);
    }
}
