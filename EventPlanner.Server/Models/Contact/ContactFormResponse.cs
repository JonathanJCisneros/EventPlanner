namespace EventPlanner.Server.Models.Contact
{
    public class ContactFormResponse
    {
        public bool Success { get; set; }

        public required string Message { get; set; }
    }
}
