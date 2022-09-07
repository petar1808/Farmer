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
    internal class SeedingConfiguration : EntityConfiguration<Seeding, int>
    {
        public override void Configure(EntityTypeBuilder<Seeding> builder)
        {
            builder
                .HasOne(x => x.ArableLand)
                .WithMany(x => x.Seedings)
                .HasForeignKey(p => p.ArableLandId);

            builder
               .HasOne(x => x.WorkingSeason)
               .WithMany()
               .HasForeignKey(p => p.WorkingSeasonId);

            builder
               .HasOne(x => x.Article)
               .WithMany()
               .HasForeignKey(p => p.ArticleId)
               .IsRequired(false);

            builder.Property(p => p.ArableLandId).IsRequired();
            builder.Property(p => p.WorkingSeasonId).IsRequired();
            builder.Property(p => p.ArticleId);

            base.Configure(builder);
        }
    }
}
