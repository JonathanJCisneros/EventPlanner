using EventPlanner.Core.Notification;
using EventPlanner.Core.User;
using EventPlanner.Repository.Interfaces;
using EventPlanner.Service.Interfaces;

namespace EventPlanner.Service
{
    public class NotificationService : INotificationService
    {
        #region Fields

        private readonly INotificationRepository _notificationRepository;
        private readonly IUserService _userService;

        #endregion Fields

        #region Constructors

        public NotificationService(INotificationRepository notificationRepository, IUserService userService)
        {
            _notificationRepository = notificationRepository;
            _userService = userService;
        }

        #endregion Constructors

        #region Private Methods



        #endregion Private Methods

        #region Public Methods

        public async Task<Notification?> Get(Guid id)
        {
            return await _notificationRepository.Get(id);
        }

        public async Task<List<Notification>> GetMany(List<Guid> ids)
        {
            return await _notificationRepository.GetMany(ids);
        }

        public async Task<List<NotificationResponse>> GetUnreadForUser(Guid id)
        {
            List<NotificationResponse> response = [];

            List<Notification> notifications = await _notificationRepository.GetUnreadForUser(id);
            List<User> authors = await _userService.GetMany(notifications.Select(x => x.AuthorId).Distinct().ToList());

            foreach (Notification notification in notifications)
            {
                User? author = authors.FirstOrDefault(x => x.Id == notification.AuthorId);

                if (author != null)
                {
                    response.Add(new NotificationResponse(notification, NotificiationStatus.Unread, $"{author.FirstName} {author.LastName}"));
                }
            }

            return response;
        }

        public async Task<List<Notification>> GetAll()
        {
            return await _notificationRepository.GetAll();
        }

        public async Task<bool> Create(Notification notification)
        {
            return await _notificationRepository.Create(notification);
        }

        public async Task<bool> Update(Notification notification)
        {
            notification.UpdatedDate = DateTime.UtcNow;

            return await _notificationRepository.Update(notification);
        }

        public Task<bool> Delete(Guid id)
        {
            return _notificationRepository.Delete(id);
        }        

        #endregion Public Methods
    }
}
