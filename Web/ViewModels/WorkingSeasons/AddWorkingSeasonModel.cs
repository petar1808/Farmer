using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.WorkingSeasonConstraints;

namespace Web.ViewModels.WorkingSeasons
{
    public class AddWorkingSeasonModel
    {
        [Required]
        [StringLength(MaxLenghtName, MinimumLength = MinLenghtName, ErrorMessage = "Сезона трябва да бъде {0} символа.")]
        [Display(Name = "Име на сезона")]
        public string Name { get; init; } = default!;

        [Display(Name = "Начало на сезона")]
        public DateTime? StartDate { get; init; }

        [Display(Name = "Край на сезона")]
        public DateTime? EndDate { get; init; }
    }
}
