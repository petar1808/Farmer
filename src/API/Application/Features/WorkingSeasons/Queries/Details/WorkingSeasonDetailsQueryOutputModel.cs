using Application.Mappings;
using Domain.Models;

namespace Application.Features.WorkingSeasons.Queries.Details
{
    public class WorkingSeasonDetailsQueryOutputModel : CommonWorkingSeasonOutputQueryModel, IMapFrom<WorkingSeason>
    {
    }
}
