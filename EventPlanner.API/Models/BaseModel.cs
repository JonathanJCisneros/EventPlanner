using System.ComponentModel.DataAnnotations;

namespace EventPlanner.API.Models
{
    public abstract class BaseModel
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}