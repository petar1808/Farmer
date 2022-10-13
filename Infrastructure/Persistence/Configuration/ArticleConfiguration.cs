using Domain.Enum;
using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.ModelConstraint.CommonConstraints;

namespace Infrastructure.Persistence.Configuration
{
    internal class ArticleConfiguration : EntityConfiguration<Article, int>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(MaxNameLenght).IsRequired();
            builder.Property(p => p.ArticleType).IsRequired();
            base.Configure(builder);
        }
    }
}
