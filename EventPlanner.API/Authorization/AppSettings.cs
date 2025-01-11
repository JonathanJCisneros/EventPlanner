namespace EventPlanner.API.Authorization
{
    public class AppSettings
    {
        public required string Secret { get; set; }

        public required string Issuer { get; set; }

        public required string Audience { get; set; }

        public int ExpirationDays { get; set; }
    }
}
