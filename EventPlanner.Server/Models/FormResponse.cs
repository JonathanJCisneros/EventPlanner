namespace EventPlanner.Server.Models
{
    public class FormResponse
    {
        public bool Success { get; set; }

        public required string Message { get; set; }
    }
}
