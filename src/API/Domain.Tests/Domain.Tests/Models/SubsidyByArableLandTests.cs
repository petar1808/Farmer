using Domain.Models;

namespace Domain.Tests.Models
{
    public class SubsidyByArableLandTests
    {
        private readonly Faker _faker;

        public SubsidyByArableLandTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var arableLandId = _faker.Random.Int(1, 100);
            var income = _faker.Random.Decimal(1, 1000);

            // Act
            var subsidyByArableLand = new SubsidyByArableLand(arableLandId, income);

            // Assert
            subsidyByArableLand.ArableLandId.Should().Be(arableLandId);
            subsidyByArableLand.Income.Should().Be(income);
            subsidyByArableLand.ArableLand.Should().BeNull(); // Assuming this is not set in the constructor
            subsidyByArableLand.Subsidy.Should().BeNull(); // Assuming this is not set in the constructor
        }

        [Fact]
        public void UpdateIncome_ShouldUpdateIncome()
        {
            // Arrange
            var subsidyByArableLand = new SubsidyByArableLand(_faker.Random.Int(1, 100), _faker.Random.Decimal(1, 1000));
            var newIncome = _faker.Random.Decimal(1, 1000);

            // Act
            subsidyByArableLand.UpdateIncome(newIncome);

            // Assert
            subsidyByArableLand.Income.Should().Be(newIncome);
        }
    }
}
