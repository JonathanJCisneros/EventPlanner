using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventPlanner.API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            string? id = User.FindFirstValue(JwtRegisteredClaimNames.Sid);

            if (id == null)
            {
                throw new Exception("Missing user id");
            }

            return Guid.Parse(id);
        }

        protected string GetEmail()
        {
            string? email = User.FindFirstValue(JwtRegisteredClaimNames.Email);

            if (email == null)
            {
                throw new Exception("Missing user email");
            }

            return email;
        }
    }
}
