using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Seedings
{
    public class EditSeedingModel
    {
        public int Id { get; init; }

        [Display(Name = "Arable Land Name")]
        public int ArableLandId { get; init; }

        public IEnumerable<SelectedListArableLand> ArableLands { get; set; } = default!;

        [Display(Name = "Article Name")]
        public int ArticleId { get; init; }

        public IEnumerable<SelectedListArticle> Articles { get; set; } = default!;

        [Display(Name = "Woriking Season Name")]
        public int WorkingSeasonId { get; init; }
    }
}
