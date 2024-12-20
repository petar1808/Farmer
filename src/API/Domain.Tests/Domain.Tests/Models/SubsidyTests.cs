﻿using Domain.Models;
using System.Reflection;

namespace Domain.Tests.Models
{
    public class SubsidyTests
    {
        private readonly Faker _faker;

        public SubsidyTests()
        {
            _faker = new Faker();
        }

        [Fact]
        public void UpdateIncome_ShouldUpdateIncomeSuccessfully()
        {
            // Arrange
            var initialIncome = _faker.Random.Decimal(100, 1000);
            var newIncome = _faker.Random.Decimal(1000, 5000);
            var subsidy = CreateSubsidy(initialIncome);

            // Act
            subsidy.UpdateIncome(newIncome);

            // Assert
            subsidy.Income.Should().Be(newIncome);
        }

        [Fact]
        public void UpdateDate_ShouldUpdateDateSuccessfully()
        {
            // Arrange
            var initialDate = _faker.Date.Past();
            var newDate = _faker.Date.Future();
            var subsidy = CreateSubsidy(date: initialDate);

            // Act
            subsidy.UpdateDate(newDate);

            // Assert
            subsidy.Date.Should().Be(newDate);
        }

        [Fact]
        public void UpdateComment_ShouldUpdateCommentSuccessfully()
        {
            // Arrange
            var initialComment = _faker.Lorem.Sentence();
            var newComment = _faker.Lorem.Sentence();
            var subsidy = CreateSubsidy(comment: initialComment);

            // Act
            subsidy.UpdateComment(newComment);

            // Assert
            subsidy.Comment.Should().Be(newComment);
        }

        private Subsidy CreateSubsidy(
            decimal? income = null,
            int? workingSeasonId = null,
            DateTime? date = null,
            List<SubsidyByArableLand>? subsidyByArableLands = null,
            string? comment = null)
        {
            return new Subsidy(
                income ?? _faker.Random.Decimal(100, 1000),
                workingSeasonId ?? _faker.Random.Int(1, 10),
                date ?? _faker.Date.Past(),
                subsidyByArableLands ?? new List<SubsidyByArableLand>(),
                comment ?? _faker.Lorem.Sentence());
        }

        [Fact]
        public void PrivateConstructor_ShouldInitializeDefaultValues()
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
    }
}

