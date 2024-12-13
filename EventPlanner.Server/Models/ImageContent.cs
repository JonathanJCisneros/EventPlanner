namespace EventPlanner.Server.Models
{
    public class ImageContent : BaseModel
    {
        public required string Source { get; set; }

        public required string AltText { get; set; }
    }
}
