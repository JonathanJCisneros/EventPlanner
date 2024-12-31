using EventPlanner.API.Authorization;
using EventPlanner.API.Models.Forms.User;
using EventPlanner.Core.Extension_Methods;
using EventPlanner.Core.User;
using EventPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        #endregion Fields

        #region Constructors

        public UserController(IUserService userService, AppSettings appSettings) 
        { 
            _userService = userService;
            _appSettings = appSettings;
        }

        #endregion Constructors

        #region Private Methods

        private string GenerateToken(AuthorizeResult result)
        {
            List<Claim> claims = 
            [
                new Claim(JwtRegisteredClaimNames.Sid, result.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, result.Name),
                new Claim(JwtRegisteredClaimNames.Email, result.Email)                
            ];

            if (result.Roles != null && result.Roles.Count() > 0)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Typ, String.Join(',', result.Roles.Select(x => EnumExtensions.GetDisplayName(x)))));
            }

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_appSettings.Secret));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _appSettings.Issuer,
                audience: _appSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_appSettings.ExpirationTimeSpan),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion Private Methods

        #region Public Methods/Actions

        [HttpPost]
        public async Task<UserFormResponse> Login([FromBody] LoginModel model)
        {
            model = model.FormatModel();

            UserFormResponse response = new()
            {
                Success = false,
                Message = "We're having technical difficulties logging you in. Please try again later!"
            };

            AuthorizeResult result = await _userService.Authorize(model.Email, model.Password);

            if (result.Id == null && !String.IsNullOrWhiteSpace(result.Message))
            {
                response.Message = result.Message;

                return response;
            }

            response.Success = true;
            response.Message = null;
            response.Token = GenerateToken(result);

            return response;
        }

        [HttpPost]
        public async Task<UserFormResponse> Register([FromBody] RegisterModel model)
        {
            model = model.FormatModel();

            UserFormResponse response = new() 
            { 
                Success = false,
                Message = "We're having technical difficulties signing you up. Please try again later!"
            };

            if (await _userService.UserExists(model.Email))
            {
                response.Message = "Email is already in use, please use another.";

                return response;
            }

            User user = model.ToUser();

            bool created = await _userService.Create(user);

            if (created)
            {
                response.Success = true;
                response.Message = null;

                AuthorizeResult result = new(user);

                response.Token = GenerateToken(result);
            }

            return response;
        }

        #endregion Public Methods/Actions
    }
}
