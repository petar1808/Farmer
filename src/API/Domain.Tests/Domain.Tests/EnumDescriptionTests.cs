using System.ComponentModel;
using System.Reflection;
using Domain.Enum;

namespace Domain.Tests.Models
{
    public class EnumDescriptionTests
    {
        [Theory]
        [InlineData(ArticleType.Seeds, "Семена")]
        [InlineData(ArticleType.Fertilizers, "Торове")]
        [InlineData(ArticleType.Preparations, "Препарати")]
        public void ArticleType_ShouldHaveCorrectDescription(ArticleType articleType, string expectedDescription)
        {
            // Act
            var description = GetEnumDescription(articleType);

            // Assert
            description.Should().Be(expectedDescription);
        }

        [Theory]
        [InlineData(ExpenseType.Fuel, "Гориво")]
        [InlineData(ExpenseType.Machinery, "Машини")]
        [InlineData(ExpenseType.Pesticides, "Препарати")]
        [InlineData(ExpenseType.Fertilizers, "Торове")]
        [InlineData(ExpenseType.Seeds, "Семена")]
        [InlineData(ExpenseType.Rent, "Рента")]
        [InlineData(ExpenseType.Harvest, "Жътва")]
        public void ExpenseType_ShouldHaveCorrectDescription(ExpenseType expenseType, string expectedDescription)
        {
            // Act
            var description = GetEnumDescription(expenseType);

            // Assert
            description.Should().Be(expectedDescription);
        }

        [Theory]
        [InlineData(WorkType.Cultivation, "Култивиране")]
        [InlineData(WorkType.Rolling, "Валиране")]
        [InlineData(WorkType.Disking, "Дискуване")]
        [InlineData(WorkType.Plowing, "Оране")]
        [InlineData(WorkType.Sowing, "Сеитба")]
        [InlineData(WorkType.Felling, "Сечка")]
        [InlineData(WorkType.Hoeing, "Окопаване")]
        [InlineData(WorkType.Harvesting, "Жътва")]
        public void WorkType_ShouldHaveCorrectDescription(WorkType workType, string expectedDescription)
        {
            // Act
            var description = GetEnumDescription(workType);

            // Assert
            description.Should().Be(expectedDescription);
        }

        [Theory]
        [InlineData(TreatmentType.Fertilization, "Торене")]
        [InlineData(TreatmentType.Spraying, "Пръскане")]
        public void TreatmentType_ShouldHaveCorrectDescription(TreatmentType treatmentType, string expectedDescription)
        {
            // Act
            var description = GetEnumDescription(treatmentType);

            // Assert
            description.Should().Be(expectedDescription);
        }

        private string? GetEnumDescription<TEnum>(TEnum enumValue) where TEnum : struct
        {
            if (enumValue.Equals(default))
            {
                return string.Empty; // Or throw an exception if null is not expected
            }

            FieldInfo? fieldInfo = enumValue!.GetType().GetField(enumValue.ToString()!);
            if (fieldInfo == null)
            {
                return enumValue.ToString() ?? string.Empty;
            }

            DescriptionAttribute? attribute = fieldInfo.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
            return attribute?.Description ?? enumValue.ToString();
        }

    }
}
