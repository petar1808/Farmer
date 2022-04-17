using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.WorkingSeasonConstraints;

namespace Web.ViewModels.WorkingSeasons
{
    public class WorkingSeasonBaseModel
    {
        [Required]
        [StringLength(MaxLenghtName, MinimumLength = MinLenghtName)]
        public string Name { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
