using Application.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Seedings
{
    public class AddSeedingViewModel
    {
        [Display(Name = "Земя")]
        public int ArableLandId { get; init; }

        public IEnumerable<SelectionListModel> ArableLands { get; set; } = default!;

        [Display(Name = "Артикул")]
        public int ArticleId { get; init; }

        public IEnumerable<SelectionListModel> Articles { get; set; } = default!;

        public int WorkingSeasonId { get; init; }
    }
}
