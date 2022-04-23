using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint;

namespace Web.ViewModels.ArableLands
{
    public class ArableLandBaseViewModel
    {
        [Required]
        [StringLength(CommonConstraints.MaxNameLenght, ErrorMessage = "Земята не може да бъде по-голям от {1} символа.")]
        [Display(Name = "Име")]
        public string Name { get; init; } = default!;

        [Display(Name = "Декари")]
        [Required]
        [Range(0,int.MaxValue,ErrorMessage ="Декарите трябва да бъдат по-големи 0.")]
       
        public int SizeInDecar { get; init; }
    }
}
