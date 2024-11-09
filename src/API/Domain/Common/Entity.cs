namespace Domain.Common
{
    public abstract class Entity<TId> : IEntity, ITenant
        where TId : struct
    {
        protected Entity(TId id)
        {
            Id = id;
        }
        protected Entity()
        {
            Id = default!;
        }
        public TId Id { get; }
        public int TenantId { get; set; }
    }
}
