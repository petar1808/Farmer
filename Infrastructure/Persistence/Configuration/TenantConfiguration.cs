using Application.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.ModelConstraint.CommonConstraints;

namespace Infrastructure.Persistence.Configuration
{
    internal class TenantConfiguration : EntityConfiguration<Tenant, int>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Name).HasMaxLength(MaxNameLenght).IsRequired();

            base.Configure(builder);
        }
    }
}
