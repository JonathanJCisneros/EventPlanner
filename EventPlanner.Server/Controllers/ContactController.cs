using EventPlanner.Server.Models;
using EventPlanner.Server.Models.Contact;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public FormResponse SubmitContact([FromBody] ContactFormModel model)
        {
            model = model.Validate();

            FormResponse response = new()
            {
                Success = false,
                Message = "Form submission failed!"
            };

            if (Validator.TryValidateObject(model, new ValidationContext(model), new List<ValidationResult>(), true))
            {
                response.Success = true;
                response.Message = "Form submission worked!";
            }

            return response;
        }

        #endregion Public Methods
    }
}