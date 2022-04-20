using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint;

namespace Web.ViewModels.ArableLands
{
    public class ArableLandBaseViewModel
    {
        [Required]
        [StringLength(CommonConstraints.MaxNameLenght)]
        [Display(Name = "Име")]
        public string Name { get; init; } = default!;

        [Display(Name = "Декари")]
        [Required]
        public int SizeInDecar { get; init; }
    }
}
