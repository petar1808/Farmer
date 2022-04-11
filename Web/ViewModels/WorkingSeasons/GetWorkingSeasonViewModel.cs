using Application.Mappings;
using Application.Models.WorkingSeasons;

namespace Web.ViewModels.WorkingSeasons
{
    public class GetWorkingSeasonViewModel : IMapFrom<GetWorkingSeasonModel>
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
