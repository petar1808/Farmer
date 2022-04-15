using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.WorkingSeasonConstraints;

namespace Web.ViewModels.WorkingSeasons
{
    public class AddWorkingSeasonModel
    {
        [Required]
        [StringLength(MinLenghtName,MinimumLength = MinLenghtName)]
        public string Name { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
