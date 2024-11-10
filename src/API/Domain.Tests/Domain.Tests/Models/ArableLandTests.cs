using Bogus;
using Domain.Models;
using Domain.Exceptions;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Models
{
    public class ArableLandTests
    {
        private readonly Faker _faker;

        public ArableLandTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void Constructor_ShouldCreateArableLand_WhenValidParameters()
        {
            // Arrange
            var name = _faker.Random.String2(10, 20);
            var size = _faker.Random.Int(1, 100);

            // Act
            var arableLand = new ArableLand(name, size);

            // Assert
            arableLand.Name.Should().Be(name);
            arableLand.SizeInDecar.Should().Be(size);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var emptyName = string.Empty;
            var size = _faker.Random.Int(1, 100);

            // Act
            var act = () => new ArableLand(emptyName, size);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*cannot be null or empty*");
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenSizeIsNonPositive()
        {
            // Arrange
            var name = _faker.Random.String2(10, 20);
            var invalidSize = 0;

            // Act
            var act = () => new ArableLand(name, invalidSize);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*must be a positive number.");
        }

        [Fact]
        public void UpdateName_ShouldUpdateName_WhenValidName()
        {
            // Arrange
            var arableLand = new ArableLand(_faker.Random.String2(10, 20), _faker.Random.Int(1, 100));
            var newName = _faker.Random.String2(5, 15);

            // Act
            arableLand.UpdateName(newName);

            // Assert
            arableLand.Name.Should().Be(newName);
        }

        [Fact]
        public void UpdateName_ShouldThrowException_WhenNameIsEmpty()
        {
            // Arrange
            var arableLand = new ArableLand(_faker.Random.String2(10, 20), _faker.Random.Int(1, 100));
            var emptyName = string.Empty;

            // Act
            var act = () => arableLand.UpdateName(emptyName);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*cannot be null or empty*");
        }

        [Fact]
        public void UpdateSizeInDecar_ShouldUpdateSize_WhenValidSize()
        {
            // Arrange
            var arableLand = new ArableLand(_faker.Random.String2(10, 20), _faker.Random.Int(1, 100));
            var newSize = _faker.Random.Int(1, 100);

            // Act
            arableLand.UpdateSizeInDecar(newSize);

            // Assert
            arableLand.SizeInDecar.Should().Be(newSize);
        }

        [Fact]
        public void UpdateSizeInDecar_ShouldThrowException_WhenSizeIsNonPositive()
        {
            // Arrange
            var arableLand = new ArableLand(_faker.Random.String2(10, 20), _faker.Random.Int(1, 100));
            var invalidSize = 0;

            // Act
            var act = () => arableLand.UpdateSizeInDecar(invalidSize);

            // Assert
            act.Should().Throw<DomainException>()
               .WithMessage("*must be a positive number*");
        }
    }
}
