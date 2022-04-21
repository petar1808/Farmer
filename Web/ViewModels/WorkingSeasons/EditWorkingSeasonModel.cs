using Application.Mappings;
using Application.Models.WorkingSeasons;
using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.WorkingSeasonConstraints;

namespace Web.ViewModels.WorkingSeasons
{
    public class EditWorkingSeasonModel : IMapFrom<GetWorkingSeasonModel>
    {
        [Display(Name = "Ид")]
        public int Id { get; init; }

        [Required]
        [StringLength(MinLenghtName, MinimumLength = MinLenghtName)]
        [Display(Name = "Име на сезон")]
        public string Name { get; init; } = default!;

        [Display(Name = "Начало на сезона")]
        public DateTime? StartDate { get; init; }

        [Display(Name = "Край на сезона")]
        public DateTime? EndDate { get; init; }
    }
}
