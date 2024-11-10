using Domain.Models;

namespace Domain.Tests.Models
{
    public class ExpenseByArableLandTests
    {
        private readonly Faker _faker;

        public ExpenseByArableLandTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
        {
            // Arrange
            var arableLandId = _faker.Random.Int(1, 100);
            var sum = _faker.Finance.Amount(1, 1000);

            // Act
            var expenseByArableLand = new ExpenseByArableLand(arableLandId, sum);

            // Assert
            expenseByArableLand.ArableLandId.Should().Be(arableLandId);
            expenseByArableLand.Sum.Should().Be(sum);
            expenseByArableLand.ArableLand.Should().BeNull();
            expenseByArableLand.Expense.Should().BeNull();
        }

        [Fact]
        public void UpdateSum_ShouldUpdateSum_WhenValidValueIsProvided()
        {
            // Arrange
            var initialSum = _faker.Finance.Amount(1, 1000);
            var expenseByArableLand = new ExpenseByArableLand(1, initialSum);
            var newSum = _faker.Finance.Amount(100, 2000);

            // Act
            expenseByArableLand.UpdateSum(newSum);

            // Assert
            expenseByArableLand.Sum.Should().Be(newSum);
        }

        [Fact]
        public void UpdateSum_ShouldReturnSameInstance()
        {
            // Arrange
            var expenseByArableLand = new ExpenseByArableLand(1, 100);

            // Act
            var result = expenseByArableLand.UpdateSum(200);

            // Assert
            result.Should().BeSameAs(expenseByArableLand);
        }
    }
}
