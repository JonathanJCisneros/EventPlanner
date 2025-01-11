using EventPlanner.Core.Event;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.API.Models.Forms
{
    public class EventModel : BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(8, ErrorMessage = "Name must be at least 8 characters")]
        [MaxLength(100, ErrorMessage = "Name can be no more than 100 characters long")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public EventType Type { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(8, ErrorMessage = "Description must be at least 8 characters long")]
        [MaxLength(3000, ErrorMessage = "Description can be no more than 3000 characters long")]
        public required string Description { get; set; }

        [RegularExpression(@"^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+\.[a-z]+(\/[a-zA-Z0-9#]+\/?)*$", ErrorMessage = "Website is invalid")]
        public string? Website { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Guest Max must be at least 0")]
        public int GuestMax { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public bool IsDigital { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [Future(ErrorMessage = "Start Date must be in the future")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Future(ErrorMessage = "End Date is ")]
        public DateTime EndDate { get; set; }

        public AddressModel? Address { get; set; }

        public EventModel FormatModel()
        {
            Name = Name.FormatFull();
            Description = Description.FormatBody();
            Website = Website?.FormatLower();
            Address = Address?.FormatModel();

            return this;
        }

        public Event ToEvent()
        {
            return new()
            {
                Id = Id,
                Name = Name,
                Type = Type,
                Description = Description,
                Website = Website,
                GuestMax = GuestMax,
                IsPublic = IsPublic,
                IsDigital = IsDigital,
                StartDate = StartDate,
                EndDate = EndDate,
                AddressId = Address?.Id,
                CreatedDate = CreatedDate,
                UpdatedDate = UpdatedDate
            };
        }
    }
}
