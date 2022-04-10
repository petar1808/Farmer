using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Application.Models.ArableLands
{
    public class ArableLandBaseModel
    {
        [Required]
        [StringLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [Display(Name = "Size In Decar")]
        [Required]
        public int SizeInDecar { get; init; }
    }
}
