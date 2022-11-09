using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.ModelConstraint.CommonConstraints;

namespace Application.Models.ArableLands
{
    public class ArablaLandBaseModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        [MaxLength(MaxNameLenght,ErrorMessage = "Името трябва да съдържа най-много 50 знака")]
        public string Name { get; init; } = default!;

        [Required(ErrorMessage = "Декарите са задължителни")]
        [Range(1, int.MaxValue,ErrorMessage = "Декарите трябва да са положително число")]
        public int SizeInDecar { get; init; }
    }
}
