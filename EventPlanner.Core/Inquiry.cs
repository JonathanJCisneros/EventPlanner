using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Core
{
    public class Inquiry : Base
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public InquirySubject Subject { get; set; }

        public InquiryStatus Status { get; set; }

        public required string Message { get; set; }
    }

    public enum InquirySubject
    {
        [Display(Name = "Question")]
        Question,
        [Display(Name = "Special Event")]
        SpecialEvent,
        [Display(Name = "Partnership")]
        Partnership,
        [Display(Name = "Ideas")]
        Ideas
    }

    public enum InquiryStatus
    {
        [Display(Name = "Unread")]
        Unread,
        [Display(Name = "Viewed")]
        Viewed,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Closed")]
        Closed
    }
}
