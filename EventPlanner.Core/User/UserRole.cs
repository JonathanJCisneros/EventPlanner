using EventPlanner.Core.Base;

namespace EventPlanner.Core.User
{
    public class UserRole : BaseEntity
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
