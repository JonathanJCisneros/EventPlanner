namespace EventPlanner.API.Models.Forms.User
{
    public class UserFormResponse : FormResponse
    {
        public string? Token { get; set; }

        public int? ExpirationDays { get; set; }
    }
}
