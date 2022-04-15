using Application.Mappings;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.WorkingSeasons
{
    public class GetWorkingSeasonModel : IMapFrom<WorkingSeason>
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
