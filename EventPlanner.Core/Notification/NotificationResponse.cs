namespace EventPlanner.Core.Notification
{
    public class NotificationResponse
    {
        public Notification Notification { get; set; }

        public NotificiationStatus Status { get; set; }

        public string Author { get; set; }

        public NotificationResponse(Notification notification, NotificiationStatus status, string author)
        {
            Notification = notification;
            Status = status;
            Author = author;
        }
    }
}