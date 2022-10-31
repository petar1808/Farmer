using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Seedings
{
    public class SownArableLandModel
    {
        public SownArableLandModel(int seedingId, string arableLandName, int sizeInDecar)
        {
            SeedingId = seedingId;
            ArableLandName = arableLandName;
            SizeInDecar = sizeInDecar;
        }

        public int SeedingId { get; set; }

        public string ArableLandName { get; set; }

        public int SizeInDecar { get; set; }
    }
}
