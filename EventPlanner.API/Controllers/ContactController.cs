using EventPlanner.API.Models.Forms;
using EventPlanner.API.Models.Forms.Contact;
using EventPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        #region Fields

        private readonly IInquiryService _inquiryService;
        private readonly ILogger<ContactController> _logger;

        #endregion Fields

        #region Constructors

        public ContactController(IInquiryService inquiryService, ILogger<ContactController> logger)
        {
            _inquiryService = inquiryService;
            _logger = logger;
        }

        #endregion Constructors

        #region Public Methods

        [HttpPost]
        public async Task<FormResponse> SubmitInquiry([FromBody] InquiryModel model)
        {
            model = model.FormatModel();

            FormResponse response = new()
            {
                Success = false,
                Message = "We're having technical difficulties submitting your message at this time. Please try again later!"
            };

            bool created = await _inquiryService.Create(model.ToInquiry());

            if (created)
            {
                response.Success = true;
                response.Message = "Your message has been received, we will be in touch!";
            }

            return response;
        }

        #endregion Public Methods
    }
}