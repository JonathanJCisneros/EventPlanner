namespace EventPlanner.Repository.Query_Builder
{
    public static class QueryBuilder
    {
        public static QueryBuilderResponse WhereQueryOneKey(List<string> values, string name, string joiner)
        {
            string query = "WHERE ";

            Dictionary<string, object?> parameters = [];

            for (int i = 0; i < values.Count - 1; i++)
            {
                query += $"{name} = @{name}{i}";

                if (i != values.Count - 1)
                {
                    query += $" {joiner} ";
                }

                parameters.Add($"@{name}{i}", values[i]);
            }

            return new(query, parameters);
        }

        public static QueryBuilderResponse WhereQueryManyKeys(Dictionary<string, string> values, string joiner)
        {
            string query = "WHERE ";

            Dictionary<string, object?> parameters = [];

            for (int i = 0; i < values.Count - 1; i++)
            {
                KeyValuePair<string, string> kvp = values.ElementAt(i);

                query += $"{kvp.Key} = @{kvp.Key}{i}";

                if (i != values.Count - 1)
                {
                    query += $" {joiner} ";
                }

                parameters.Add($"@{kvp.Key}{i}", kvp.Value);
            }

            return new(query, parameters);
        }

        public static QueryBuilderResponse WherePairsQuery(Dictionary<string, string> values, string keyName, string valueName, string innerJoiner, string joiner)
        {
            string query = "WHERE ";

            Dictionary<string, object?> parameters = [];

            for (int i = 0; i < values.Count - 1; i++)
            {
                KeyValuePair<string, string> kvp = values.ElementAt(i);

                query += $"({keyName} = @{keyName}{i} {innerJoiner} {valueName} = @{valueName}{i})";

                if (i != values.Count - 1)
                {
                    query += $" {joiner} ";
                }

                parameters.Add($"{keyName}{i}", $"{kvp.Key}");

                parameters.Add($"{valueName}{i}", $"{kvp.Value}");
            }

            return new(query, parameters);
        }
    }    
}