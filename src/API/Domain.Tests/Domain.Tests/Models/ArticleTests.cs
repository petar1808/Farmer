using Domain.Models;
using Domain.Enum;
using Domain.Exceptions;
using static Domain.ModelConstraint;

namespace Domain.Tests.Models
{
    public class ArticleTests
    {
        private readonly Faker _faker;

        public ArticleTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldCreateArticle_WhenValidParameters()
        {
            // Arrange
            var name = _faker.Random.String2(5, 20);
            var articleType = ArticleType.Fertilizers;

            // Act
            var article = new Article(name, articleType);

            // Assert
            article.Name.Should().Be(name);
            article.ArticleType.Should().Be(articleType);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var emptyName = string.Empty;
            var articleType = ArticleType.Seeds;

            // Act
            var act = () => new Article(emptyName, articleType);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*cannot be null or empty*");
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameExceedsMaxLength()
        {
            // Arrange
            var longName = _faker.Random.String2(CommonConstraints.MaxNameLenght + 1);
            var articleType = ArticleType.Fertilizers;

            // Act
            var act = () => new Article(longName, articleType);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*must have a maximum of*");
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenArticleTypeIsInvalid()
        {
            // Arrange
            var name = _faker.Random.String2(5, 20);
            var invalidArticleType = (ArticleType)999; // An invalid enum value

            // Act
            var act = () => new Article(name, invalidArticleType);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*is not a valid value for*");
        }

        [Fact]
        public void UpdateName_ShouldUpdateName_WhenValidName()
        {
            // Arrange
            var article = new Article(_faker.Random.String2(5, 20), ArticleType.Seeds);
            var newName = _faker.Random.String2(5, 20);

            // Act
            article.UpdateName(newName);

            // Assert
            article.Name.Should().Be(newName);
        }

        [Fact]
        public void UpdateName_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var article = new Article(_faker.Random.String2(5, 20), ArticleType.Fertilizers);
            var emptyName = string.Empty;

            // Act
            var act = () => article.UpdateName(emptyName);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*cannot be null or empty*");
        }

        [Fact]
        public void UpdateArticleType_ShouldUpdateArticleType_WhenValidArticleType()
        {
            // Arrange
            var article = new Article(_faker.Random.String2(5, 20), ArticleType.Seeds);
            var newArticleType = ArticleType.Fertilizers;

            // Act
            article.UpdateArticleType(newArticleType);

            // Assert
            article.ArticleType.Should().Be(newArticleType);
        }

        [Fact]
        public void UpdateArticleType_ShouldThrowException_WhenArticleTypeIsInvalid()
        {
            // Arrange
            var article = new Article(_faker.Random.String2(5, 20), ArticleType.Seeds);
            var invalidArticleType = (ArticleType)999; // An invalid enum value

            // Act
            var act = () => article.UpdateArticleType(invalidArticleType);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*is not a valid value for*");
        }
    }
}
