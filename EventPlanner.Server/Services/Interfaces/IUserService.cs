using EventPlanner.Server.Models;
using EventPlanner.Server.Models.Authentication;

namespace EventPlanner.Server.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticationResponse? Authenticate(AuthenticationRequest model);

        IEnumerable<User> GetAll();

        User? GetById(Guid id);
    }
}
