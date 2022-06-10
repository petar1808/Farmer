using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Seedings
{
    public class AddSeedingViewModel
    {
        public int ArableLandId { get; set; }

        public IEnumerable<SelectionListModel> ArableLands { get; set; } = default!;

        public int ArticleId { get; init; }

        public IEnumerable<SelectionListModel> Articles { get; set; } = default!;

        public int WorkingSeasonId { get; set; }
    }
}
