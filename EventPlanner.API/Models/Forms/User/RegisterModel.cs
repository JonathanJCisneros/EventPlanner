using System.ComponentModel.DataAnnotations;

namespace EventPlanner.API.Models.Forms.User
{
    public class RegisterModel : BaseModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "First Name must be no more than 50 characters long")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters long")]
        [MaxLength(50, ErrorMessage = "Last Name must be no more than 50 characters long")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be a valid address")]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "Email must be a valid address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^(\(?[0-9]{3}\)?)\s?([0-9]{3})\-?([0-9]{4})$", ErrorMessage = "Phone Number must be valid")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(128, ErrorMessage = "Password must be no more than 128 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must have at least one upper case character, one lowercase character, one number and one special character")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public required string ConfirmPassword { get; set; }

        public RegisterModel FormatModel()
        {
            FirstName = FirstName.FormatWord(); 
            LastName = LastName.FormatWord();
            Email = Email.FormatLower();
            PhoneNumber = PhoneNumber.FormatPhoneNumber();
            Password = !String.IsNullOrWhiteSpace(Password) ? Password.Trim() : Password;

            return this;
        }
    }
}
