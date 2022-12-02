using Together.Core.Helpers;

namespace Together.Core.Domain
{
    public class EntityBase
    {
        public Guid Id { get; protected init; } = Guid.NewGuid();
        public DateTime CreatedOn { get; protected init; } = DateTimeHelper.NewDateTime();
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool Active { get; set; }
    }
}
