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
    public class SubsidyByArableLandConfiguration : EntityConfiguration<SubsidyByArableLand, int>
    {
        public override void Configure(EntityTypeBuilder<SubsidyByArableLand> builder)
        {
            builder
                .HasOne(x => x.ArableLand)
                .WithMany()
                .HasForeignKey(p => p.ArableLandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Income).HasColumnType("decimal(12,2)").IsRequired();
            base.Configure(builder);
        }
    }
}
