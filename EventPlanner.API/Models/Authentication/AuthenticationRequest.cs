namespace EventPlanner.API.Models.Authentication
{
    public class AuthenticationRequest
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
