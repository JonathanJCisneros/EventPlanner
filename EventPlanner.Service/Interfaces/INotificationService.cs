using EventPlanner.Core.Base;
using EventPlanner.Core.Notification;

namespace EventPlanner.Service.Interfaces
{
    public interface INotificationService : IBaseInterface<Notification>
    {
        Task<List<NotificationResponse>> GetUnreadForUser(Guid id);
    }
}
