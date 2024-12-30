namespace EventPlanner.Core.User
{
    public class User : Base
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Password { get; set; }

        public required string Salt { get; set; }

        public required UserRoles[] Roles { get; set; }

        public bool IsAuthorized { get; set; }

        public bool IsActive { get; set; }

        public int PasswordAttempts { get; set; }

        public DateTime LastLoggedIn { get; set; }

        public DateTime? LastLoggedOut { get; set; }
    }    
}
