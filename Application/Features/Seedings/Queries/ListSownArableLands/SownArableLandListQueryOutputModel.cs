using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Seedings.Queries.ListSownArableLands
{
    public class SownArableLandListQueryOutputModel : IMapFrom<Seeding>
    {
        public int SeedingId { get; set; }

        public string ArableLandName { get; set; } = default!;

        public int SizeInDecar { get; set; }

        public int ArableLandId { get; set; }

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<Seeding, SownArableLandListQueryOutputModel>()
                .ForMember(x => x.SeedingId, cfg => cfg.MapFrom(c => c.Id))
                .ForMember(x => x.SizeInDecar, cfg => cfg.MapFrom(c => c.ArableLand.SizeInDecar))
                .ForMember(x => x.ArableLandName, cfg => cfg.MapFrom(c => c.ArableLand.Name));
    }
}
