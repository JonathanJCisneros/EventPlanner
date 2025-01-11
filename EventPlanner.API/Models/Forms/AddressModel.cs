using EventPlanner.Core.Address;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.API.Models.Forms
{
    public class AddressModel : BaseModel
    {
        [Required(ErrorMessage = "Name for Address is required")]
        [MinLength(3, ErrorMessage = "Name for Address must be at least 3 characters long")]
        [MaxLength(100, ErrorMessage = "Name for Address can be no more than 100 characters long")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Street Line One is required")]
        [MinLength(3, ErrorMessage = "Street Line One must be at least 3 characters long")]
        [MaxLength(45, ErrorMessage = "Street Line One can be no more than 45 characters long")]
        public required string StreetLineOne { get; set; }

        [MinLength(3, ErrorMessage = "Street Line Two must be at least 3 characters long")]
        [MaxLength(45, ErrorMessage = "Street Line Two can be no more than 45 characters long")]
        public string? StreetLineTwo { get; set; }

        [MinLength(3, ErrorMessage = "Street Line Three must be at least 3 characters long")]
        [MaxLength(45, ErrorMessage = "Street Line Three can be no more than 45 characters long")]
        public string? StreetLineThree { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(45, ErrorMessage = "City can be no more than 45 characters long")]
        public required string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [Range(2, 2, ErrorMessage = "State must be 2 characters long")]
        public required string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [MinLength(5, ErrorMessage = "Postal Code must be at least 5 characters long")]
        [MaxLength(10, ErrorMessage = "Postal Code can be no more than 10 characters long")]
        public required string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [Range(2, 2, ErrorMessage = "Country must be 2 characters")]
        public required string Country { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$", ErrorMessage = "Phone Number is invalid")]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [MaxLength(254, ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        public AddressModel FormatModel()
        {
            Name = Name.FormatFull();
            StreetLineOne = StreetLineOne.FormatUpper();
            StreetLineTwo = StreetLineTwo?.FormatUpper();
            StreetLineThree = StreetLineThree?.FormatUpper();
            City = City.FormatUpper();
            PhoneNumber = PhoneNumber.FormatPhoneNumber();
            Email = Email.FormatLower();

            return this;
        }

        public Address ToAddress()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                StreetLineOne = StreetLineOne,
                StreetLineTwo = StreetLineTwo,
                StreetLineThree = StreetLineThree,
                City = City,
                State = State,
                PostalCode = PostalCode,
                Country = Country,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Coordinates = new()
                {
                    Latitude = Latitude,
                    Longitude = Longitude
                },
                CreatedDate = CreatedDate,
                UpdatedDate = UpdatedDate
            };
        }
    }
}
