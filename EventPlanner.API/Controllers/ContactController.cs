using EventPlanner.API.Models.Forms;
using EventPlanner.API.Models.Forms.Contact;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.API.Controllers
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
        public FormResponse SubmitInquiry([FromBody] InquiryModel model)
        {
            model = model.FormatModel();

            FormResponse response = new()
            {
                Success = false,
                Message = "Form submission failed!"
            };

            if (Validator.TryValidateObject(model, new ValidationContext(model), [], true))
            {
                response.Success = true;
                response.Message = "Form submission worked!";
            }

            return response;
        }

        #endregion Public Methods
    }
}