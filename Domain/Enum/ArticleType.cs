using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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