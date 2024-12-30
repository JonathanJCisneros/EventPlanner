namespace EventPlanner.Core.Notification
{
    public class Notification : Base
    {
        public required string Title { get; set; }

        public string? Subject { get; set; }

        public required string Description { get; set; }

        public Guid EventId { get; set; }

        public Guid AuthorId { get; set; }
    }
}
