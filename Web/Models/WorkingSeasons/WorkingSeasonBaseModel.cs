using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Web.Models.WorkingSeasons
{
    public class WorkingSeasonBaseModel
    {
        [Required]
        [StringLength(MaxLenghtNameWorkingSeason, MinimumLength = MinLenghtNameWorkingSeason)]
        public string Name { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
