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
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [Required]
        [Range(1, int.MaxValue)]
        public int SizeInDecar { get; init; }
    }
}
