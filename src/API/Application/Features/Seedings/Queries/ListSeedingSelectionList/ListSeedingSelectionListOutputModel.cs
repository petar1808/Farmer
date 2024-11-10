using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Seedings.Queries.ListSeeding
{
    public class ListSeedingSelectionListOutputModel : IMapFrom<Seeding>
    {
        public int Value { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<Seeding, ListSeedingSelectionListOutputModel>()
               .ForMember(x => x.Value, cfg => cfg.MapFrom(c => c.Id))
               .ForMember(
                x => x.Name,
                cfg => cfg.MapFrom(c => $"Сезон:{c.WorkingSeason.Name}, Земя:{c.ArableLand.Name}")
                );
    }
}
