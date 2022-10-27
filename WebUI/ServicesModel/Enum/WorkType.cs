using System.ComponentModel;

namespace WebUI.ServicesModel.Enum
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
        [Description("Жътва")]
        Harvest,
        [Description("Сеитба")]
        Sowing,
        [Description("Сечка")]
        Felling
    }
}
