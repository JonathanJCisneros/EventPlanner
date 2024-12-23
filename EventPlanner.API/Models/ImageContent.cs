namespace EventPlanner.API.Models
{
    public class ImageContent : BaseModel
    {
        public required string Source { get; set; }

        public required string AltText { get; set; }
    }
}
