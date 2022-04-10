namespace Domain.Common
{
    public abstract class Entity<TId>
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
        public TId Id { get;}
    }
}
