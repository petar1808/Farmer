using System.Reflection;
using Domain.Common;
using Domain.Models;

namespace Domain.Tests.Common
{
    public class CommonEntitiesTests
    {
        [Fact]
        public void Entity_DefaultConstructor_ShouldSetDefaultId()
        {
            // Arrange & Act
            var entity = new TestEntity();

            // Assert
            entity.Id.Should().Be(default(int));
            entity.TenantId.Should().Be(default(int));
        }

        [Fact]
        public void Entity_ConstructorWithId_ShouldSetId()
        {
            // Arrange
            int expectedId = 5;

            // Act
            var entity = new TestEntity(expectedId);

            // Assert
            entity.Id.Should().Be(expectedId);
            entity.TenantId.Should().Be(default(int));
        }

        [Fact]
        public void Entity_ShouldAllowSettingTenantId()
        {
            // Arrange
            var entity = new TestEntity();
            int tenantId = 123;

            // Act
            entity.TenantId = tenantId;

            // Assert
            entity.TenantId.Should().Be(tenantId);
        }

        [Fact]
        public void Subsidy_PrivateConstructor_ShouldInitializeDefaultValues()
        {
            // Act: Use reflection to invoke the private constructor
            var subsidy = (Subsidy)Activator.CreateInstance(
                typeof(Subsidy),
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                null,
                null)!;

            // Assert: Check that all default values are set correctly
            subsidy.Income.Should().Be(default);
            subsidy.Date.Should().Be(default);
            subsidy.WorkingSeasonId.Should().Be(default);
            subsidy.Comment.Should().Be(default);
            subsidy.SubsidyByArableLands.Should().BeNull();
        }

        [Fact]
        public void Subsidy_Constructor_WithParameters_ShouldInitializeProperties()
        {
            // Arrange
            decimal income = 1000m;
            int workingSeasonId = 1;
            DateTime date = DateTime.Now;
            var subsidyByArableLands = new List<SubsidyByArableLand>();
            string comment = "Initial subsidy comment";

            // Act
            var subsidy = new Subsidy(income, workingSeasonId, date, subsidyByArableLands, comment);

            // Assert
            subsidy.Income.Should().Be(income);
            subsidy.WorkingSeasonId.Should().Be(workingSeasonId);
            subsidy.Date.Should().Be(date);
            subsidy.SubsidyByArableLands.Should().BeSameAs(subsidyByArableLands);
            subsidy.Comment.Should().Be(comment);
        }

        private class TestEntity : Entity<int>
        {
            public TestEntity() : base() { }

            public TestEntity(int id) : base(id) { }
        }
    }
}
