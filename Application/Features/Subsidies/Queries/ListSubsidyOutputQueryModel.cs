using Application.Features.WorkingSeasons.Queries.List;
using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Subsidies.Queries
{
    public class ListSubsidyOutputQueryModel : CommonSubsidyOutputQueryModel, IMapFrom<Subsidy>
    {
        public List<SubsidySlitByArableLand> ArableLands { get; set; } = new List<SubsidySlitByArableLand>();

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Subsidy, ListSubsidyOutputQueryModel>()
                .ForMember(x => x.ArableLands, cfg => cfg.MapFrom(c => c.Seeding.ArableLand.Name));
    }

    public class SubsidySlitByArableLand
    {
        public string ArableLandName { get; set; } = string.Empty;

        public decimal Income { get; set; }
    }
}
