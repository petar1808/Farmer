using Domain.Models;

namespace Domain.Tests.Models
{
    public class SeedingTests
    {
        private readonly Faker _faker;

        public SeedingTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var arableLandId = _faker.Random.Int(1, 100);
            var workingSeasonId = _faker.Random.Int(1, 100);

            // Act
            var seeding = new Seeding(arableLandId, workingSeasonId);

            // Assert
            seeding.ArableLandId.Should().Be(arableLandId);
            seeding.WorkingSeasonId.Should().Be(workingSeasonId);
            seeding.ArticleId.Should().BeNull();
            seeding.SeedsQuantityPerDecare.Should().Be(0);
            seeding.HarvestedQuantityPerDecare.Should().Be(0);
            seeding.HarvestedGrainSellingPricePerKilogram.Should().Be(0);
            seeding.Treatments.Should().BeNull();
            seeding.PerformedWorks.Should().BeNull();
        }

        [Fact]
        public void UpdateArticle_ShouldUpdateArticleId()
        {
            // Arrange
            var seeding = new Seeding(_faker.Random.Int(1, 100), _faker.Random.Int(1, 100));
            var articleId = _faker.Random.Int(1, 100);

            // Act
            seeding.UpdateArticle(articleId);

            // Assert
            seeding.ArticleId.Should().Be(articleId);
        }

        [Fact]
        public void UpdateSeedsQuantityPerDecare_ShouldUpdateSeedsQuantityPerDecare()
        {
            // Arrange
            var seeding = new Seeding(_faker.Random.Int(1, 100), _faker.Random.Int(1, 100));
            var seedsQuantity = _faker.Random.Decimal(0.1M, 100M);

            // Act
            seeding.UpdateSeedsQuantityPerDecare(seedsQuantity);

            // Assert
            seeding.SeedsQuantityPerDecare.Should().Be(seedsQuantity);
        }

        [Fact]
        public void UpdateHarvestedQuantityPerDecare_ShouldUpdateHarvestedQuantityPerDecare()
        {
            // Arrange
            var seeding = new Seeding(_faker.Random.Int(1, 100), _faker.Random.Int(1, 100));
            var harvestedQuantity = _faker.Random.Int(1, 500);

            // Act
            seeding.UpdateHarvestedQuantityPerDecare(harvestedQuantity);

            // Assert
            seeding.HarvestedQuantityPerDecare.Should().Be(harvestedQuantity);
        }

        [Fact]
        public void UpdateHarvestedGrainSellingPricePerKilogram_ShouldUpdateHarvestedGrainSellingPricePerKilogram()
        {
            // Arrange
            var seeding = new Seeding(_faker.Random.Int(1, 100), _faker.Random.Int(1, 100));
            var sellingPrice = _faker.Random.Decimal(0.5M, 50M);

            // Act
            seeding.UpdateHarvestedGrainSellingPricePerKilogram(sellingPrice);

            // Assert
            seeding.HarvestedGrainSellingPricePerKilogram.Should().Be(sellingPrice);
        }
    }
}
