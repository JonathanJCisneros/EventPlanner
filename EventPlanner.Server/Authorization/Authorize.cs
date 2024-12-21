using EventPlanner.Server.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Server.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public JsonResult AuthorizationFail 
        {
            get 
            { 
                return new JsonResult(new Error("Unauthorized access, please check if you are logged in and try again"))
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            } 
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Items.ContainsKey("User"))
            {
                context.Result = AuthorizationFail;

                return;
            }

            User? user = (User?)context.HttpContext.Items["User"];

            if (user == null)
            {
                context.Result = AuthorizationFail;

                return;
            }
        }
    }
}
