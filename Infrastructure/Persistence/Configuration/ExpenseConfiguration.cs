using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class ExpenseConfiguration : EntityConfiguration<Expense, int>
    {
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(p => p.Date).IsRequired();
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Sum).HasColumnType("decimal(12,2)").IsRequired();
            builder.Property(p => p.PricePerUnit).HasColumnType("decimal(12,2)").IsRequired();
            builder.Property(p => p.Quantity).HasColumnType("decimal(12,2)").IsRequired();

            builder
                .HasOne(x => x.Article)
                .WithMany()
                .HasForeignKey(x => x.ArticleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne<WorkingSeason>()
                .WithMany()
                .HasForeignKey(x => x.WorkingSeasonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.ExpenseByArableLands)
                .WithOne()
                .HasForeignKey(x => x.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
