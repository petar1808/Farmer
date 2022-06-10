using Application.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.WorkingSeasons
{
    public class GetWorkingSeasonModel : WorkingSeasonBaseModel, IMapFrom<WorkingSeason>
    {
        public int Id { get; init; }
    }
}
