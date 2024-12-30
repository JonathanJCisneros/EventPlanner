namespace EventPlanner.Core.User
{
    public class UserRole : Base
    {
        public UserRoles Role { get; set; }

        public Guid UserId { get; set; }
    }

    public enum UserRoles
    {
        Admin,
        User
    }
}
