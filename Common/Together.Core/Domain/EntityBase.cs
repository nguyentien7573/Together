namespace Together.Core.Domain
{
    public class EntityBase
    {
        public Guid Id { get; protected init; } = Guid.NewGuid();
        public DateTime CreatedOn { get; protected init; } = DateTime.UtcNow;
        public Guid CreatedBy { get;protected set; }
        public DateTime? UpdatedOn { get; protected set; }
        public Guid? UpdatedBy { get; protected set; }
    }
}
