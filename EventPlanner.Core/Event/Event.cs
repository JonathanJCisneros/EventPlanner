﻿using EventPlanner.Core.Base;

namespace EventPlanner.Core.Event
{
    public class Event : BaseEntity
    {
        public required string Name { get; set; }

        public EventType Type { get; set; }

        public required string Description { get; set; }

        public string? Website { get; set; }

        public bool IsPublic { get; set; }

        public bool IsDigital { get; set; }

        public int GuestMax { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid? AddressId { get; set; }
    }

    public enum EventType
    {
        Wedding,
        Birthday,
        Business        
    }
}