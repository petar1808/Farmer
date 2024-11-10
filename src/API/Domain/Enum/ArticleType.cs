using System.ComponentModel;

namespace Domain.Enum
{
    public enum ArticleType
    {
        [Description("Семена")]
        Seeds = 1,
        [Description("Торове")]
        Fertilizers = 2,
        [Description("Препарати")]
        Preparations = 3
    }
}