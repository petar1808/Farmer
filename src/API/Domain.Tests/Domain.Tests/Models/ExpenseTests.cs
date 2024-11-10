using Domain.Models;
using Domain.Enum;

namespace Domain.Tests.Models
{
    public class ExpenseTests
    {
        private readonly Faker _faker;

        public ExpenseTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldCreateExpense_WhenValidParameters()
        {
            // Arrange
            var date = DateTime.Now;
            var type = ExpenseType.Seeds; 
            var pricePerUnit = _faker.Finance.Amount(1, 100);
            var quantity = _faker.Finance.Amount(1, 10);
            int? articleId = 1;
            int workingSeasonId = 2024;

            // Act
            var expense = new Expense(date, type, pricePerUnit, quantity, articleId, workingSeasonId);

            // Assert
            expense.Date.Should().Be(date);
            expense.Type.Should().Be(type);
            expense.PricePerUnit.Should().Be(pricePerUnit);
            expense.Quantity.Should().Be(quantity);
            expense.Sum.Should().Be(pricePerUnit * quantity);
            expense.ArticleId.Should().Be(articleId);
            expense.WorkingSeasonId.Should().Be(workingSeasonId);
        }

        [Fact]
        public void AddExpenseByArableLands_ShouldAddArableLands()
        {
            // Arrange
            var expense = CreateSampleExpense();
            var arableLands = new List<ExpenseByArableLand>
            {
                new ExpenseByArableLand(1, 100),
                new ExpenseByArableLand(2, 150)
            };

            // Act
            expense.AddExpenseByArableLands(arableLands);

            // Assert
            expense.ExpenseByArableLands.Should().HaveCount(2);
            expense.ExpenseByArableLands.Should().BeEquivalentTo(arableLands);
        }

        [Fact]
        public void UpdateExpenseByArableLands_ShouldUpdateExistingAndAddNewArableLands()
        {
            // Arrange
            var expense = CreateSampleExpense();
            var existingArableLand = new ExpenseByArableLand(1, 100);
            expense.AddExpenseByArableLands(new List<ExpenseByArableLand> { existingArableLand });

            var updatedArableLands = new List<ExpenseByArableLand>
            {
                new ExpenseByArableLand(1, 200), // Updated sum for existing
                new ExpenseByArableLand(2, 150)  // New arable land
            };

            // Act
            expense.UpdateExpenseByArableLands(updatedArableLands);

            // Assert
            expense.ExpenseByArableLands.Should().HaveCount(2);
            expense.ExpenseByArableLands.Should().ContainSingle(a => a.ArableLandId == 1 && a.Sum == 200);
            expense.ExpenseByArableLands.Should().ContainSingle(a => a.ArableLandId == 2 && a.Sum == 150);
        }

        [Fact]
        public void UpdatePricePerUnit_ShouldRecalculateSum()
        {
            // Arrange
            var expense = CreateSampleExpense();
            var newPricePerUnit = _faker.Finance.Amount(1, 100);

            // Act
            expense.UpdatePricePerUnit(newPricePerUnit);

            // Assert
            expense.PricePerUnit.Should().Be(newPricePerUnit);
            expense.Sum.Should().Be(newPricePerUnit * expense.Quantity);
        }

        [Fact]
        public void UpdateQuantity_ShouldRecalculateSum()
        {
            // Arrange
            var expense = CreateSampleExpense();
            var newQuantity = _faker.Finance.Amount(1, 20);

            // Act
            expense.UpdateQuantity(newQuantity);

            // Assert
            expense.Quantity.Should().Be(newQuantity);
            expense.Sum.Should().Be(expense.PricePerUnit * newQuantity);
        }

        [Fact]
        public void UpdateDate_ShouldUpdateDate()
        {
            // Arrange
            var expense = CreateSampleExpense();
            var newDate = DateTime.Now.AddDays(5);

            // Act
            expense.UpdateDate(newDate);

            // Assert
            expense.Date.Should().Be(newDate);
        }

        [Fact]
        public void UpdateType_ShouldUpdateExpenseType()
        {
            // Arrange
            var expense = CreateSampleExpense();
            var newType = ExpenseType.Fertilizers;

            // Act
            expense.UpdateType(newType);

            // Assert
            expense.Type.Should().Be(newType);
        }

        private Expense CreateSampleExpense()
        {
            var date = DateTime.Now;
            var type = ExpenseType.Seeds;
            var pricePerUnit = _faker.Finance.Amount(1, 100);
            var quantity = _faker.Finance.Amount(1, 10);
            int? articleId = 1;
            int workingSeasonId = 2024;

            return new Expense(date, type, pricePerUnit, quantity, articleId, workingSeasonId);
        }
    }
}
