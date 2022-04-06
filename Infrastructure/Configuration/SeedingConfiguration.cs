using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    internal class SeedingConfiguration : EntityConfiguration<Seeding, int>
    {
        public override void Configure(EntityTypeBuilder<Seeding> builder)
        {
            builder
                .HasOne<ArableLand>()
                .WithMany()
                .HasForeignKey(p => p.ArableLandId);
            builder
               .HasOne<WorkingSeason>()
               .WithMany()
               .HasForeignKey(p => p.WorkingSeasonId);
            builder.Property(p => p.ArableLandId).IsRequired();
            builder.Property(p => p.WorkingSeasonId).IsRequired();
            builder.Property(p => p.ArticleId).IsRequired();
            base.Configure(builder);
        }
    }
}
