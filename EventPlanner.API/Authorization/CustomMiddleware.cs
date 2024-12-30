using EventPlanner.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EventPlanner.API.Authorization
{
    public class CustomMiddleware
    {
        #region Fields

        private readonly RequestDelegate next;
        private readonly AppSettings appSettings;

        #endregion Fields

        #region Constructors

        public CustomMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            this.next = next;
            this.appSettings = appSettings.Value;
        }

        #endregion Constructors

        #region Private Methods

        private void AttachUserToContext(HttpContext context, string token)
        {
            //try
            //{
            //    JwtSecurityTokenHandler tokenHandler = new();
            //    byte[] key = Encoding.ASCII.GetBytes(this.appSettings.Secret);

            //    tokenHandler.ValidateToken(token, new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        ClockSkew = TimeSpan.Zero
            //    }, out SecurityToken validatedToken);

            //    JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            //    Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            //    context.Items["User"] = userService.GetById(userId);
            //}
            //catch
            //{
            //    Debug.WriteLine("Token retrieval failed");
            //}
        }

        #endregion Private Methods

        #region Public Methods

        public async Task InvokeAsync(HttpContext context)
        {
            int status = StatusCodes.Status200OK;
            string? error = null;

            if (context.Request.Headers.Accept.Count == 0)
            {
                status = StatusCodes.Status406NotAcceptable;
                error = "Missing Accept Header in Request";
            }
            else if (!context.Request.Headers.Accept.Contains("application/json"))
            {
                status = StatusCodes.Status406NotAcceptable;
                error = "Invalid Accept Header in Request";
            }
            else if (context.Request.Headers.ContentType.Count == 0)
            {
                status = StatusCodes.Status415UnsupportedMediaType;
                error = "Missing Content Type Header in Request";
            }
            else if (!context.Request.Headers.ContentType.Contains("application/json"))
            {
                status = StatusCodes.Status415UnsupportedMediaType;
                error = "Invalid Content Type Header in Request";
            }

            if (status != StatusCodes.Status200OK && !String.IsNullOrWhiteSpace(error)) 
            { 
                context.Response.StatusCode = status;
                await context.Response.WriteAsJsonAsync(new Error(error));
                return;
            }

            await this.next(context);
        }

        #endregion Public Methods        
    }
}
