using System.ComponentModel;

namespace WebUI.ServicesModel.Enum
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
