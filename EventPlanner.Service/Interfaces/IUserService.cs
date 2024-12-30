using EventPlanner.Core.User;

namespace EventPlanner.Service.Interfaces
{
    public interface IUserService : IBaseInterface<User>
    {
        Task<bool> UserExists(string email);

        Task<AuthorizeResult> Authorize(string email, string password);
    }
}
