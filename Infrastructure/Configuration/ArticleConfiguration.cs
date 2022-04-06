﻿using Domain.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Domain.ModelConstraint.CommonConstraints;

namespace Infrastructure.Configuration
{
    internal class ArticleConfiguration : EntityConfiguration<Article, int>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(NameLenght).IsRequired();
            builder.Property(p => p.ArticleType).IsRequired();
            base.Configure(builder);
        }
    }
}
