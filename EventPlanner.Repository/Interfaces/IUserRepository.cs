using EventPlanner.Core.User;

namespace EventPlanner.Repository.Interfaces
{
    public interface IUserRepository : IBaseInterface<User>
    {
        Task<User?> CheckByEmail(string email);

        Task Login(Guid id);

        Task Logout(Guid id);
    }
}
