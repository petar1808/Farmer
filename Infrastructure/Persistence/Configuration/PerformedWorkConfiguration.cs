using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class PerformedWorkConfiguration : EntityConfiguration<PerformedWork, int>
    {
        public override void Configure(EntityTypeBuilder<PerformedWork> builder)
        {
            builder
               .HasOne(x => x.Seeding)
               .WithMany(x => x.PerformedWorks)
               .HasForeignKey(p => p.SeedingId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.SeedingId).IsRequired();
            builder.Property(p => p.WorkType).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            base.Configure(builder);
        }
    }
}
