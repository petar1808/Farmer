using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class SubsidyConfiguration : EntityConfiguration<Subsidy, int>
    {
        public override void Configure(EntityTypeBuilder<Subsidy> builder)
        {
            builder
               .HasOne(x => x.Seeding)
               .WithMany(x => x.Subsidies)
               .HasForeignKey(p => p.SeedingId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Income).HasColumnType("decimal(12,2)").IsRequired();
            builder.Property(p => p.Date).IsRequired();

            base.Configure(builder);
        }
    }
}
