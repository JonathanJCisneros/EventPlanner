using EventPlanner.Server.Models.Contact;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        #region Fields

        private readonly ILogger<ContactController> logger;

        #endregion Fields

        #region Constructors

        public ContactController(ILogger<ContactController> logger)
        {
            this.logger = logger;
        }

        #endregion Constructors

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