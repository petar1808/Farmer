using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class ConnectionStrings
    {
        public string SqlDefaultConnection { get; set; } = default!;

        public string SqlLiteConncetion { get; set; } = default!;
    }
}
