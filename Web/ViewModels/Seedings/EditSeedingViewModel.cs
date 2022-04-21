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

        public int ArableLandId { get; init; }

        public IEnumerable<SelectionListModel> ArableLands { get; set; } = default!;

        public int ArticleId { get; init; }

        public IEnumerable<SelectionListModel> Articles { get; set; } = default!;

        public int WorkingSeasonId { get; init; }
    }
}
