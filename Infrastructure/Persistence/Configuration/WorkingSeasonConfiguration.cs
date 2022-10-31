using Domain.Models;
using static Domain.ModelConstraint.CommonConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;

namespace Infrastructure.Persistence.Configuration
{
    internal class WorkingSeasonConfiguration : EntityConfiguration<WorkingSeason, int>
    {
        public override void Configure(EntityTypeBuilder<WorkingSeason> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(p => p.Name).HasMaxLength(MaxNameLenght).IsRequired(); 
            base.Configure(builder);
        }
    }
}
