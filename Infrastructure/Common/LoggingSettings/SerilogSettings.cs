using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.LoggingSettings
{
    public class SerilogSettings
    {
        public MinimumLevelConfiguration MinimumLevel { get; set; } = default!;

        public ConsoleConfiguration Console { get; set; } = new ConsoleConfiguration();

        public FileConfiguration File { get; set; } = new FileConfiguration();
    }
}
