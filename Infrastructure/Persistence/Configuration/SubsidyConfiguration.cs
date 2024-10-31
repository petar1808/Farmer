using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class SubsidyConfiguration : EntityConfiguration<Subsidy, int>
    {
        public override void Configure(EntityTypeBuilder<Subsidy> builder)
        {
            builder
                .HasMany(x => x.SubsidyByArableLands)
                .WithOne(x => x.Subsidy)
                .HasForeignKey(x => x.SubsidyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<WorkingSeason>()
                .WithMany()
                .HasForeignKey(x => x.WorkingSeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Income).HasColumnType("decimal(12,2)").IsRequired();
            builder.Property(p => p.Date).IsRequired();

            base.Configure(builder);
        }
    }
}
