using Domain.Models;
using Domain.Enum;

namespace Domain.Tests.Models
{
    public class PerformedWorkTests
    {
        private readonly Faker _faker;

        public PerformedWorkTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties_WhenValidParametersAreProvided()
        {
            // Arrange
            var seedingId = _faker.Random.Int(1, 100);
            var workType = _faker.PickRandom<WorkType>();
            var date = _faker.Date.Past();

            // Act
            var performedWork = new PerformedWork(seedingId, workType, date);

            // Assert
            performedWork.SeedingId.Should().Be(seedingId);
            performedWork.WorkType.Should().Be(workType);
            performedWork.Date.Should().Be(date);
            performedWork.Seeding.Should().BeNull();
        }

        [Fact]
        public void UpdateWorkType_ShouldUpdateWorkType_WhenValidValueIsProvided()
        {
            // Arrange
            var performedWork = new PerformedWork(1, WorkType.Plowing, DateTime.Now);
            var newWorkType = WorkType.Harvesting;

            // Act
            performedWork.UpdateWorkType(newWorkType);

            // Assert
            performedWork.WorkType.Should().Be(newWorkType);
        }

        [Fact]
        public void UpdateWorkType_ShouldReturnSameInstance()
        {
            // Arrange
            var performedWork = new PerformedWork(1, WorkType.Plowing, DateTime.Now);

            // Act
            var result = performedWork.UpdateWorkType(WorkType.Harvesting);

            // Assert
            result.Should().BeSameAs(performedWork);
        }

        [Fact]
        public void UpdateDate_ShouldUpdateDate_WhenValidValueIsProvided()
        {
            // Arrange
            var performedWork = new PerformedWork(1, WorkType.Plowing, DateTime.Now);
            var newDate = _faker.Date.Future();

            // Act
            performedWork.UpdateDate(newDate);

            // Assert
            performedWork.Date.Should().Be(newDate);
        }

        [Fact]
        public void UpdateDate_ShouldReturnSameInstance()
        {
            // Arrange
            var performedWork = new PerformedWork(1, WorkType.Plowing, DateTime.Now);

            // Act
            var result = performedWork.UpdateDate(DateTime.Now.AddDays(1));

            // Assert
            result.Should().BeSameAs(performedWork);
        }
    }
}
