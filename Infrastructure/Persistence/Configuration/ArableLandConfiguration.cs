using static Domain.ModelConstraint.CommonConstraints;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Configuration
{
    internal class ArableLandConfiguration : EntityConfiguration<ArableLand,int>
    {
        public override void Configure(EntityTypeBuilder<ArableLand> builder)
        {        
            builder.Property(p => p.Name).HasMaxLength(MaxNameLenght).IsRequired();
            builder.Property(p => p.SizeInDecar).IsRequired();
            base.Configure(builder);
        }
    }
}
