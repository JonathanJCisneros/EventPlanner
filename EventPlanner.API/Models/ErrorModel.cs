namespace EventPlanner.API.Models
{
    public class ErrorModel
    {
        public bool Success 
        {
            get 
            {
                return String.IsNullOrWhiteSpace(Message);
            } 
        }

        public string? Message { get; set; }

        public ErrorModel(string message)
        {
            Message = message;
        }
    }
}