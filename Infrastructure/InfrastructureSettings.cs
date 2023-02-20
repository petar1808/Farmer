using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class InfrastructureSettings
    {
        public string Secret { get; set; } = default!;

        public DatabaseProvider DatabaseProvider { get; set; }
    }

    public enum DatabaseProvider
    {
        SqlLite,
        SqlServer,
        MySql
    }
}
