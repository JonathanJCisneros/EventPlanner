using EventPlanner.Core.Base;

namespace EventPlanner.Core.Address
{
    public class Address : BaseEntity
    {
        public required string Name { get; set; }

        public required string StreetLineOne { get; set; }

        public string? StreetLineTwo { get; set; }

        public string? StreetLineThree { get; set; }

        public required string City { get; set; }

        public required string State { get; set; }

        public required string PostalCode { get; set; }

        public required string Country { get; set; }

        public required string PhoneNumber { get; set; }

        public required string Email { get; set; }

        public required GeoCoordinates Coordinates { get; set; }
    }
}
