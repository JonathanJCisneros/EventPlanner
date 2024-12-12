using EventPlanner.Server.Models.Contact;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactController(ILogger<ContactController> logger) : ControllerBase
    {
        #region Properties

        private readonly ILogger<ContactController> logger = logger;

        #endregion Properties

        #region Public Methods

        [HttpPost]
        public ContactFormResponse SubmitContact([FromBody] ContactFormModel model)
        {
            ContactFormResponse response = new()
            {
                Success = false,
                Message = "Form submission failed!"
            };

            if (model != null)
            {
                response.Success = true;
                response.Message = "Form submission worked!";
            }

            return response;
        }

        #endregion Public Methods
    }
}