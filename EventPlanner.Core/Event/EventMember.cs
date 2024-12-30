namespace EventPlanner.Core.Event
{
    public class EventMember : Base
    {
        public MemberType Type { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }
    }

    public enum MemberType
    {
        Host,
        Attendee
    }
}