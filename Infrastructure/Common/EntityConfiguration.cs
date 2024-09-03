using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common
{
    public abstract class EntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : Entity<TKey>
        where TKey : struct
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);

            if (typeof(ITenant).IsAssignableFrom(builder.Metadata.ClrType))
            {
                builder.Property((nameof(ITenant.TenantId))).IsRequired();
            }
        }
    }
}
