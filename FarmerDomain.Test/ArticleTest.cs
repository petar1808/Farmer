using Domain.Enum;
using Domain.Exceptions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FarmerDomain.Test
{
    public class ArticleTest
    {
        [Fact]
        public void AddArticleNameAndArticleTypeIsvalid()
        {
            var article = new Article("pshenica", ArticleType.Fertilizers);

            Assert.Equal("pshenica", article.Name);
            Assert.Equal(ArticleType.Fertilizers, article.ArticleType);
        }

        [Fact]
        public void UpdateArticleNameAndArticleTypeIsValid()
        {
            var article = new Article("пшеница", ArticleType.Fertilizers);

            article.UpdateName("слънчоглед");
            article.UpdateArticleType(ArticleType.Preparations);

            Assert.Equal("слънчоглед", article.Name);
            Assert.Equal(ArticleType.Preparations, article.ArticleType);
        }

        [Fact]
        public void UpdateArticleNameAndArticleTypeIsInvalid()
        {
            var article = new Article("пшеница", ArticleType.Fertilizers);

            Assert.Throws<DomainException>(() => article.UpdateName(null!));
            Assert.Throws<DomainException>(() => article.UpdateArticleType(0));
        }

        [Fact]
        public void UpdateArticleNameIsInvalid()
        {
            var article = new Article("пшеница", ArticleType.Fertilizers);

            Assert.Throws<DomainException>(() => article.UpdateName("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
        }
    }
}
