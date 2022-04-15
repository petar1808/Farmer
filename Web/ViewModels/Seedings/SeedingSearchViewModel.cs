using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Seedings
{
    public class SeedingSearchViewModel
    {
        public SeedingSearchViewModel(
            int seasonId, 
            IEnumerable<SeedingsGetViewModel> seedings)
        {
            this.Seedings = seedings;
            this.WorkingSeasonId = seasonId;
        }
        public int WorkingSeasonId { get; } 

        public IEnumerable<SeedingsGetViewModel> Seedings { get; } 
    }
}
