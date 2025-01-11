using EventPlanner.API.Models.Forms;
using EventPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : BaseController
    {
        #region Fields

        private readonly IEventService _eventService;
        private readonly IAddressService _addressService;

        #endregion Fields

        #region Constructors

        public EventController(IEventService eventService, IAddressService addressService) 
        { 
            _eventService = eventService;
            _addressService = addressService;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods

        [HttpPost]
        public async Task<FormResponse> CreateEvent([FromBody] EventModel model)
        {
            model = model.FormatModel();

            FormResponse response = new()
            {
                Success = false,
                Message = "We're having technical difficulties creating your event at this time. Please try again later!"
            };

            bool created = await _eventService.Create(model.ToEvent());

            if (created) 
            {
                response.Success = true;
                response.Message = "Your event has been created, redirecting!";

                if (model.Address != null) 
                {
                    _ = await _addressService.Create(model.Address.ToAddress());
                }
            }

            return response;
        }

        #endregion Public Methods
    }
}
