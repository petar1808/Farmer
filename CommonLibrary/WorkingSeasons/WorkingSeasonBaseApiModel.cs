using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.WorkingSeasons
{
    public class WorkingSeasonBaseApiModel
    {
        public string Name { get; set; } = default!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
