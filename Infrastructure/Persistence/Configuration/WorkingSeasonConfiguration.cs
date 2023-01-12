using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class WorkingSeasonConfiguration : EntityConfiguration<WorkingSeason, int>
    {
        public override void Configure(EntityTypeBuilder<WorkingSeason> builder)
        {
            builder.HasIndex(x => new
            {
                x.Name,
                x.TenantId
            }).IsUnique();

            builder.Property(p => p.Name).IsRequired(); 
            base.Configure(builder);
        }
    }
}
