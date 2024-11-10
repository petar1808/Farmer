using Application.Mappings;
using Domain.Enum;
using Domain.Models;

namespace Application.Features.PerformedWorks.Queries.Details
{
    public class PerformedWorkDetailsQueryOutputModel : CommonPerformedWorkOutputQueryModel, IMapFrom<PerformedWork>
    {
        public WorkType WorkType { get; init; } = default!;
    }
}
