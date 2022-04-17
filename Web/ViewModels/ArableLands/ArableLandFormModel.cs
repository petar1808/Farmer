using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint.CommonConstraints;

namespace Web.ViewModels.ArableLands
{
    public class ArableLandFormModel 
    {
        [Required]
        [StringLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [Display(Name = "Size In Decar")]
        [Required]
        public int SizeInDecar { get; init; }
    }
}
