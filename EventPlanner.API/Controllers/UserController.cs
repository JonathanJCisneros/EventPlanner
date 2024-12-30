using EventPlanner.API.Models.Forms;
using EventPlanner.API.Models.Forms.User;
using EventPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly IUserService _userService;

        #endregion Fields

        #region Constructors

        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods/Actions

        [HttpPost]
        public async Task<FormResponse> Register([FromBody] RegisterModel model)
        {
            model = model.FormatModel();

            FormResponse response = new() 
            { 
                Success = false,
                Message = "Form submission failed!"
            };

            if (await _userService.UserExists(model.Email))
            {
                response.Message = "Email is already in use, please use another.";

                return response;
            }

            //TODO: Save user then return token

            return response;
        }

        #endregion Public Methods/Actions
    }
}
