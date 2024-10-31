using System.ComponentModel;

namespace Domain.Enum
{
    public enum ExpenseType
    {
        [Description("Гориво")]
        Fuel = 1,
        [Description("Машини")]
        Machinery,
        [Description("Препарати")]
        Pesticides,
        [Description("Торове")]
        Fertilizers,
        [Description("Семена")]
        Seeds,
        [Description("Рента")]
        Rent,
        [Description("Жътва")]
        Harvest
    }
}
