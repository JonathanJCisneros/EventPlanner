using System.ComponentModel.DataAnnotations;

namespace EventPlanner.API.Models.Forms.Contact
{
    public class InquiryModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [MaxLength(80, ErrorMessage = "Name can be no more than 80 characters long")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid address")]
        [MaxLength(254, ErrorMessage = "Email must be no more than 254 characters long")]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "Email must be a valid address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public InquiryType Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [MinLength(5, ErrorMessage = "Message must be at least 5 characters")]
        [MaxLength(300, ErrorMessage = "Message must be no more than 300 characters long")]
        public required string Message { get; set; }

        public InquiryModel FormatModel()
        {
            Name = Name.FormatFull();
            Email = Email.FormatLower();
            Message = Message.FormatBody();

            return this;
        }
    }

    public enum InquiryType
    {
        Question,
        SpecialEvent,
        Partnership,
        Ideas
    }
}