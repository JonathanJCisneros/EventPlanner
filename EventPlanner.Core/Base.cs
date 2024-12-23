namespace EventPlanner.Core
{
    public abstract class Base
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}