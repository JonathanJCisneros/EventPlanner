using EventPlanner.Core.Notification;

namespace EventPlanner.API.Models
{
    public class NotificationModel : BaseModel
    {
        public string Title { get; set; }

        public string? Subject { get; set; }

        public string Description { get; set; }

        public NotificiationStatus Status { get; set; }

        public string Author { get; set; }

        public NotificationModel(NotificationResponse response)
        {
            Id = response.Notification.Id;
            Title = response.Notification.Title;
            Subject = response.Notification.Subject;
            Description = response.Notification.Description;
            Status = response.Status;
            Author = response.Author;
            CreatedDate = response.Notification.CreatedDate;
            UpdatedDate = response.Notification.UpdatedDate;
        }
    }
}
