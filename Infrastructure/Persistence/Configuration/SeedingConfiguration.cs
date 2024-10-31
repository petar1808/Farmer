using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class SeedingConfiguration : EntityConfiguration<Seeding, int>
    {
        public override void Configure(EntityTypeBuilder<Seeding> builder)
        {
            builder
                .HasOne(x => x.ArableLand)
                .WithMany(x => x.Seedings)
                .HasForeignKey(p => p.ArableLandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.WorkingSeason)
               .WithMany(x => x.Seedings)
               .HasForeignKey(p => p.WorkingSeasonId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(x => new
                {
                    x.ArableLandId,
                    x.WorkingSeasonId
                }).IsUnique();

            builder
               .HasOne(x => x.Article)
               .WithMany()
               .HasForeignKey(p => p.ArticleId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired(false);

            builder.Property(p => p.ArableLandId).IsRequired();
            builder.Property(p => p.WorkingSeasonId).IsRequired();
            builder.Property(p => p.HarvestedGrainSellingPricePerKilogram).HasColumnType("decimal(12,2)");
            builder.Property(p => p.SeedsQuantityPerDecare).HasColumnType("decimal(12,2)");
            builder.Property(p => p.ArticleId);

            base.Configure(builder);
        }
    }
}
