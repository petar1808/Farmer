﻿using Application.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Seedings
{
    public class AddSeedingViewModel
    {

        [Display(Name = "Arable Land Name")]
        public int ArableLandId { get; init; }

        public IEnumerable<SelectionListModel> ArableLands { get; set; } = default!;

        [Display(Name = "Article Name")]
        public int ArticleId { get; init; }

        public IEnumerable<SelectionListModel> Articles { get; set; } = default!;

        [Display(Name = "Woriking Season Name")]
        public int WorkingSeasonId { get; init; }
    }
}
