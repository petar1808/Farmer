using AutoMapper;
using Domain.Models;

namespace Application.Features.Subsidies.Queries.List
{
    public class ListSubsidyOutputQueryModel : CommonSubsidyOutputQueryModel
    {
        public Dictionary<string, decimal> IncomeByArableLand { get; set; } = new Dictionary<string, decimal>();

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<Subsidy, ListSubsidyOutputQueryModel>()
                .ForMember(x => x.IncomeByArableLand,
                           cfg => cfg.MapFrom(c => c.SubsidyByArableLands.ToDictionary(k => k.ArableLand.Name, v => v.Income)));
        }
    }
}
