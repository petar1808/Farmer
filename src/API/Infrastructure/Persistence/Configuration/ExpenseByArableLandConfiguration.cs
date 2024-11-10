using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class ExpenseByArableLandConfiguration : EntityConfiguration<ExpenseByArableLand, int>
    {
        public override void Configure(EntityTypeBuilder<ExpenseByArableLand> builder)
        {
            builder.Property(p => p.Sum).HasColumnType("decimal(12,2)").IsRequired();

            builder
                .HasOne(x => x.ArableLand)
                .WithMany()
                .HasForeignKey(p => p.ArableLandId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
