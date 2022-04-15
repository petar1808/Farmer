using Application.Mappings;
using Application.Models.Common;
using Application.Models.Seedings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Seedings
{
    public class EditSeedingViewModel 
    {
        public int Id { get; init; }

        [Display(Name = "Arable Land Name")]
        public int ArableLandId { get; init; }

        public IEnumerable<SelectionListModel> ArableLands { get; set; } = default!;

        [Display(Name = "Article Name")]
        public int ArticleId { get; init; }

        public IEnumerable<SelectionListModel> Articles { get; set; } = default!;

        [Display(Name = "Woriking Season Name")]
        public int WorkingSeasonId { get; init; }
    }
}
