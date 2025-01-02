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

        #endregion Fields

        #region Constructors

        public EventController(IEventService eventService) 
        { 
            _eventService = eventService;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods



        #endregion Public Methods
    }
}
