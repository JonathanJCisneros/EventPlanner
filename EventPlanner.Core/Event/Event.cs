namespace EventPlanner.Core.Event
{
    public class Event : Base
    {
        public required string Name { get; set; }

        public EventType Type { get; set; }

        public required string Description { get; set; }

        public int GuestMax { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid AddressId { get; set; }
    }

    public enum EventType
    {
        Wedding,
        Birthday,
        Business        
    }
}