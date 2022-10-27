using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    internal class TreatmentConfiguration : EntityConfiguration<Treatment, int>
    {
        public override void Configure(EntityTypeBuilder<Treatment> builder)
        {
            builder
               .HasOne(x => x.Seeding)
               .WithMany(x => x.Treatments)
               .HasForeignKey(p => p.SeedingId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasOne(x => x.Article)
              .WithMany()
              .HasForeignKey(p => p.ArticleId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.FuelPrice).HasColumnType("decimal(12,2)").IsRequired(false);
            builder.Property(p => p.ArticleQuantity).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.TreatmentType).IsRequired();
            builder.Property(p => p.ArticlePrice).HasColumnType("decimal(12,2)").IsRequired();
            base.Configure(builder);
        }
    }
}
