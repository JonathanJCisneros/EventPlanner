namespace EventPlanner.Server.Authorization
{
    public class Error
    {
        public bool Success 
        {
            get 
            {
                return String.IsNullOrWhiteSpace(Message);
            } 
        }

        public string? Message { get; set; }

        public Error(string message)
        {
            Message = message;
        }
    }
}