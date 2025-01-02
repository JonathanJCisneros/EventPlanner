using EventPlanner.Core.Base;
using EventPlanner.Core.Notification;

namespace EventPlanner.Repository.Interfaces
{
    public interface INotificationRepository : IBaseInterface<Notification>
    {
        Task<List<Notification>> GetUnreadForUser(Guid id);
    }
}
