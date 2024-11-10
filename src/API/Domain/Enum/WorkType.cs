using System.ComponentModel;

namespace Domain.Enum
{
    public enum WorkType
    {
        [Description("Култивиране")]
        Cultivation = 1,
        [Description("Валиране")]
        Rolling,
        [Description("Дискуване")]
        Disking,
        [Description("Оране")]
        Plowing,
        [Description("Сеитба")]
        Sowing,
        [Description("Сечка")]
        Felling,
        [Description("Окопаване")]
        Hoeing,
        [Description("Жътва")]
        Harvesting
    }
}
