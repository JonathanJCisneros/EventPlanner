namespace EventPlanner.Repository.Query_Builder
{
    public class QueryBuilderResponse
    {
        public string Query { get; set; }

        public Dictionary<string, object?> Parameters { get; set; }

        public QueryBuilderResponse(string query, Dictionary<string, object?> parameters)
        {
            Query = query;
            Parameters = parameters;
        }
    }
}