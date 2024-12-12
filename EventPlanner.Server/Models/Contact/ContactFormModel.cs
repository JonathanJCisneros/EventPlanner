namespace EventPlanner.Server.Models.Contact
{
    public class ContactFormModel
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public ContactFormType Subject { get; set; }

        public required string Message { get; set; }
    }

    public enum ContactFormType
    {
        Question,
        SpecialEvent,
        Partnership,
        Ideas
    }
}