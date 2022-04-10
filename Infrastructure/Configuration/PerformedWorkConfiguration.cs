﻿using Domain.Models;
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
    internal class PerformedWorkConfiguration : EntityConfiguration<PerformedWork, int>
    {
        public override void Configure(EntityTypeBuilder<PerformedWork> builder)
        {
            builder
               .HasOne(x => x.Seeding)
               .WithMany()
               .HasForeignKey(p => p.SeedingId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Article)
               .WithMany()
               .HasForeignKey(p => p.ArticleId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.SeedingId).IsRequired();
            builder.Property(p => p.WorkType).IsRequired();
            builder.Property(p => p.ArticleId).IsRequired();
            base.Configure(builder);
        }
    }
}
