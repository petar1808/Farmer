using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.WorkingSeasons.Queries.List
{
    public class WorkingSeasonBalanceListQueryOutputModel : CommonWorkingSeasonOutputQueryModel, IMapFrom<WorkingSeason>
    {

    }
}
