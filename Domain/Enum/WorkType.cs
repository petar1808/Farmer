using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Description("Жътва")]
        Harvest,
        [Description("Сеитба")]
        Sowing
    }
}
