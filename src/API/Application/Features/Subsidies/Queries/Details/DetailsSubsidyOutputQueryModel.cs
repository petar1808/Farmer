using AutoMapper;
using Domain.Models;

namespace Application.Features.Subsidies.Queries.Details
{
    public class DetailsSubsidyOutputQueryModel : CommonSubsidyOutputQueryModel
    {
        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<Subsidy, DetailsSubsidyOutputQueryModel>()
                .ForMember(x => x.SelectedArableLands, cfg => cfg.MapFrom(c => c.SubsidyByArableLands.Select(x => x.ArableLandId)));
        }
    }
}
