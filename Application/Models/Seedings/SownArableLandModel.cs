using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Seedings
{
    public class SownArableLandModel
    {
        public SownArableLandModel(int seedingId, string arableLandName)
        {
            SeedingId = seedingId;
            ArableLandName = arableLandName;
        }

        public int SeedingId { get; set; }

        public string ArableLandName { get; set; }
    }
}
