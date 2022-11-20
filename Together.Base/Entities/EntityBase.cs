using Together.Base.Interface;

namespace Together.Base.Entities
{
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        public virtual TId Id { get; protected set; }
    }
}
