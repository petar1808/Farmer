using Application.Features.Seedings.Queries.ListSownArableLands;
using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.WorkingSeasons.Queries.List
{
    public class WorkingSeasonListQueryOutputModel : CommonWorkingSeasonOutputQueryModel, IMapFrom<WorkingSeason>
    {
        public List<string> SownArableLnds { get; set; } = new ();

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<WorkingSeason, WorkingSeasonListQueryOutputModel>()
                    .ForMember(x => x.SownArableLnds, cfg => cfg.MapFrom(c => c.Seedings.Select(x => x.ArableLand.Name)));
    }
}
