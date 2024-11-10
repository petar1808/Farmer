using Domain.Exceptions;
using Domain.Models;

namespace Domain.Tests.Models
{
    public class WorkingSeasonTests
    {
        private readonly Faker _faker;

        public WorkingSeasonTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_WithValidNameAndDates_ShouldCreateWorkingSeason()
        {
            // Arrange
            string validName = _faker.Random.String2(9); // Within MinLenghtName and MaxLenghtName
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddMonths(6);

            // Act
            var workingSeason = new WorkingSeason(validName, startDate, endDate);

            // Assert
            workingSeason.Name.Should().Be(validName);
            workingSeason.StartDate.Should().Be(startDate);
            workingSeason.EndDate.Should().Be(endDate);
        }

        [Fact]
        public void Constructor_WithInvalidNameLength_ShouldThrowDomainException()
        {
            // Arrange
            string invalidName = _faker.Random.String2(10); // Exceeds MaxLenghtName

            // Act
            Action action = () => new WorkingSeason(invalidName);

            // Assert
            action.Should().Throw<DomainException>()
                .WithMessage($"*{nameof(WorkingSeason.Name)} must be between {ModelConstraint.WorkingSeasonConstraints.MinLenghtName} and {ModelConstraint.WorkingSeasonConstraints.MaxLenghtName}*");
        }

        [Fact]
        public void UpdateName_WithValidName_ShouldUpdateName()
        {
            // Arrange
            var workingSeason = new WorkingSeason(_faker.Random.String2(9));
            string newValidName = _faker.Random.String2(9);

            // Act
            workingSeason.UpdateName(newValidName);

            // Assert
            workingSeason.Name.Should().Be(newValidName);
        }

        [Fact]
        public void UpdateName_WithInvalidNameLength_ShouldThrowDomainException()
        {
            // Arrange
            var workingSeason = new WorkingSeason(_faker.Random.String2(9));
            string invalidName = _faker.Random.String2(10); // Exceeds MaxLenghtName

            // Act
            Action action = () => workingSeason.UpdateName(invalidName);

            // Assert
            action.Should().Throw<DomainException>()
                .WithMessage($"*{nameof(WorkingSeason.Name)} must be between {ModelConstraint.WorkingSeasonConstraints.MinLenghtName} and {ModelConstraint.WorkingSeasonConstraints.MaxLenghtName}*");
        }

        [Fact]
        public void UpdateStartDate_ShouldUpdateStartDate()
        {
            // Arrange
            var workingSeason = new WorkingSeason(_faker.Random.String2(9), DateTime.Now, DateTime.Now.AddMonths(6));
            DateTime newStartDate = DateTime.Now.AddMonths(1);

            // Act
            workingSeason.UpdateSratDate(newStartDate);

            // Assert
            workingSeason.StartDate.Should().Be(newStartDate);
        }

        [Fact]
        public void UpdateEndDate_ShouldUpdateEndDate()
        {
            // Arrange
            var workingSeason = new WorkingSeason(_faker.Random.String2(9), DateTime.Now, DateTime.Now.AddMonths(6));
            DateTime newEndDate = DateTime.Now.AddMonths(12);

            // Act
            workingSeason.UpdateEndDate(newEndDate);

            // Assert
            workingSeason.EndDate.Should().Be(newEndDate);
        }
    }
}
