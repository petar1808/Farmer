using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Seedings
{
    public class SownArableLandModel : IMapFrom<Seeding>
    {
        
        public int SeedingId { get; set; }

        public string ArableLandName { get; set; } = default!;

        public int SizeInDecar { get; set; }

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<Seeding, SownArableLandModel>()
                .ForMember(x => x.SeedingId, cfg => cfg.MapFrom(c => c.Id))
                .ForMember(x => x.SizeInDecar, cfg => cfg.MapFrom(c => c.ArableLand.SizeInDecar))
                .ForMember(x => x.ArableLandName, cfg => cfg.MapFrom(c => c.ArableLand.Name));
    }
}
