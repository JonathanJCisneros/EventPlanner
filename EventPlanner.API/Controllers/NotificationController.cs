using EventPlanner.API.Models;
using EventPlanner.Core.Notification;
using EventPlanner.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        #region Fields

        private readonly INotificationService _notificationService;

        #endregion Fields

        #region Constructors

        public NotificationController(INotificationService notificationService) 
        { 
            _notificationService = notificationService;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods

        [Authorize]
        [HttpGet]
        public async Task<List<NotificationModel>> GetUnread()
        {
            Guid id = GetUserId();

            List<NotificationResponse> notifications = await _notificationService.GetUnreadForUser(id);

            return notifications.Select(x => new NotificationModel(x)).ToList();
        }

        #endregion Public Methods
    }
}
