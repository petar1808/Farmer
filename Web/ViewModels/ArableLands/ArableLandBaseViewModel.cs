﻿using System.ComponentModel.DataAnnotations;
using static Domain.ModelConstraint;

namespace Web.ViewModels.ArableLands
{
    public class ArableLandBaseViewModel
    {
        [Required]
        [StringLength(CommonConstraints.MaxNameLenght)]
        public string Name { get; init; } = default!;

        [Display(Name = "Size In Decar")]
        [Required]
        public int SizeInDecar { get; init; }
    }
}