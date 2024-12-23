namespace EventPlanner.API.Models.Forms
{
    public class FormResponse
    {
        public bool Success { get; set; }

        public required string Message { get; set; }
    }
}
