namespace EventPlanner.Core.User
{
    public class User : Base
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required UserRole[] Roles { get; set; }

        public bool IsAuthorized { get; set; }

        public bool IsActive { get; set; }

        public int PasswordAttempts { get; set; }

        public DateTime LastLoggedIn { get; set; }

        public DateTime? LastLoggedOut { get; set; }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}
