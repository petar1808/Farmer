using static Domain.ModelConstraint.CommonConstraints;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.Configuration
{
    internal class ArableLandConfiguration : EntityConfiguration<ArableLand,int>
    {
        public override void Configure(EntityTypeBuilder<ArableLand> builder)
        {        
            builder.Property(p => p.Name).HasMaxLength(NameLenght).IsRequired();
            builder.Property(p => p.SizeInDecar).IsRequired();
            base.Configure(builder);
        }
    }
}
