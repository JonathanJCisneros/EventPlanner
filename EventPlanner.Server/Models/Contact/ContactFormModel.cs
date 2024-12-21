using EventPlanner.Server.Extension_Methods;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Server.Models.Contact
{
    public class ContactFormModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])")]
        public required string Email { get; set; }

        [Required]
        public ContactFormType Subject { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(350)]
        public required string Message { get; set; }

        public ContactFormModel Validate()
        {
            Name = Name.FormatFull();
            Email = Email.FormatLower();
            Message = Message.FormatBody();

            return this;
        }
    }

    public enum ContactFormType
    {
        Question,
        SpecialEvent,
        Partnership,
        Ideas
    }
}