
using System.ComponentModel;


namespace Domain.Enum
{
    public enum TreatmentType
    {
        [Description("Торене")]
        Fertilization = 1,
        [Description("Пръскане")]
        Spraying = 2
    }
}
