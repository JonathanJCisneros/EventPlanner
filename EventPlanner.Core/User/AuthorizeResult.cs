namespace EventPlanner.Core.User
{
    public class AuthorizeResult
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public UserRole[]? Roles { get; set; }

        public bool PasswordMatched { get; set; }

        public bool RememberRequest { get; set; }

        public bool LockedOut { get; set; }

        public bool IsAuthorized
        {
            get => Id != null && PasswordMatched && !LockedOut;
        }

        public AuthorizeResult()
        {
            PasswordMatched = false;
            LockedOut = false;
        }

        public AuthorizeResult(User user)
        {
            Id = user.Id;
            Name = $"{user.FirstName} {user.LastName}";
            Roles = user.Roles;
            LockedOut = user.IsActive && !user.IsAuthorized;
        }
    }
}
