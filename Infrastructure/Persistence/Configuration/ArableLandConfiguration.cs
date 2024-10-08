﻿using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.ModelConstraint.CommonConstraints;

namespace Infrastructure.Persistence.Configuration
{
    internal class ArableLandConfiguration : EntityConfiguration<ArableLand, int>
    {
        public override void Configure(EntityTypeBuilder<ArableLand> builder)
        {
            builder.HasIndex(x => new
            {
                x.Name,
                x.TenantId
            }).IsUnique();

            builder.Property(p => p.Name).HasMaxLength(MaxNameLenght).IsRequired();
            builder.Property(p => p.SizeInDecar).IsRequired();
            base.Configure(builder);
        }
    }
}
