using System;
using Bogus;
using Domain.Models;
using Domain.Enum;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Models
{
    public class TreatmentTests
    {
        private readonly Faker _faker;

        public TreatmentTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
        {
            // Arrange
            var date = _faker.Date.Past();
            var treatmentType = _faker.PickRandom<TreatmentType>();
            var articleId = _faker.Random.Int(1, 100);
            var articleQuantity = _faker.Random.Decimal(1, 100);
            var seedingId = _faker.Random.Int(1, 100);

            // Act
            var treatment = new Treatment(date, treatmentType, articleId, articleQuantity, seedingId);

            // Assert
            treatment.Date.Should().Be(date);
            treatment.TreatmentType.Should().Be(treatmentType);
            treatment.ArticleId.Should().Be(articleId);
            treatment.ArticleQuantity.Should().Be(articleQuantity);
            treatment.SeedingId.Should().Be(seedingId);
        }

        [Fact]
        public void UpdateТreatmentType_ShouldUpdateTreatmentType_WhenValidValueIsProvided()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);
            var newTreatmentType = TreatmentType.Fertilization;

            // Act
            treatment.UpdateТreatmentType(newTreatmentType);

            // Assert
            treatment.TreatmentType.Should().Be(newTreatmentType);
        }

        [Fact]
        public void UpdateТreatmentType_ShouldReturnSameInstance()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);

            // Act
            var result = treatment.UpdateТreatmentType(TreatmentType.Fertilization);

            // Assert
            result.Should().BeSameAs(treatment);
        }

        [Fact]
        public void UpdateDate_ShouldUpdateDate_WhenValidValueIsProvided()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);
            var newDate = _faker.Date.Future();

            // Act
            treatment.UpdateDate(newDate);

            // Assert
            treatment.Date.Should().Be(newDate);
        }

        [Fact]
        public void UpdateDate_ShouldReturnSameInstance()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);

            // Act
            var result = treatment.UpdateDate(DateTime.Now.AddDays(1));

            // Assert
            result.Should().BeSameAs(treatment);
        }

        [Fact]
        public void UpdateArticle_ShouldUpdateArticleId_WhenValidValueIsProvided()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);
            var newArticleId = _faker.Random.Int(2, 100);

            // Act
            treatment.UpdateArticle(newArticleId);

            // Assert
            treatment.ArticleId.Should().Be(newArticleId);
        }

        [Fact]
        public void UpdateArticle_ShouldReturnSameInstance()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);

            // Act
            var result = treatment.UpdateArticle(2);

            // Assert
            result.Should().BeSameAs(treatment);
        }

        [Fact]
        public void UpdateArticleQuantity_ShouldUpdateArticleQuantity_WhenValidValueIsProvided()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);
            var newQuantity = _faker.Random.Decimal(1, 100);

            // Act
            treatment.UpdateArticleQuantity(newQuantity);

            // Assert
            treatment.ArticleQuantity.Should().Be(newQuantity);
        }

        [Fact]
        public void UpdateArticleQuantity_ShouldReturnSameInstance()
        {
            // Arrange
            var treatment = new Treatment(DateTime.Now, TreatmentType.Spraying, 1, 10, 1);

            // Act
            var result = treatment.UpdateArticleQuantity(15);

            // Assert
            result.Should().BeSameAs(treatment);
        }
    }
}
