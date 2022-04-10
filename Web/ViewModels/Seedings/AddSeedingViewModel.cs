using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Seedings
{
    public class AddSeedingViewModel
    {
        [Display(Name = "Arable Land Name")]
        public int ArableLandId { get; init; }

        public IEnumerable<SelectedListArableLand> ArableLands { get; set; } = default!;

        [Display(Name = "Article Name")]
        public int ArticleId { get; init; }

        public IEnumerable<SelectedListArticle> Articles { get; set; } = default!;

        [Display(Name = "Woriking Season Name")]
        public int WorkingSeasonId { get; init; }

        public IEnumerable<SelectedListWorkingSeaoson> WorkingSeaosons { get; set; } = default!;

    }
}
