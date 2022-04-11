using Application.Mappings;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.WorkingSeasons
{
    public class GetWorkingSeasonModel : WorkingSeasonBaseModel, IMapFrom<WorkingSeason>
    {
        [Required]
        public int Id { get; init; }
    }
}
