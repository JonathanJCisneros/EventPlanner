namespace EventPlanner.Core.Notification
{
    public class Recipient : Base
    {
        public NotificiationStatus Status { get; set; }

        public Guid NotificationId { get; set; }

        public Guid UserId { get; set; }
    }

    public enum NotificiationStatus
    {
        Unread,
        Read
    }
}
