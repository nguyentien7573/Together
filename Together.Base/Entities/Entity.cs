namespace Together.Base.Entities
{
    public abstract class Entity : EntityBase<int>
    {
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
    }
}
