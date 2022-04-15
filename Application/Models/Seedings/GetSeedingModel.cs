using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Models.Seedings
{
    public class GetSeedingModel : IMapFrom<Seeding>
    {
        public int Id { get; init; }

        public int ArableLandId { get; set; }

        public string AreableLandName { get; init; } = default!;

        public string ArticleName { get; init; } = default!;

        public int ArticleId { get; set; }

        public int SizeInDecar { get; init; } = default!;

        public string SeasonName { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Seeding, GetSeedingModel>()
                .ForMember(x => x.AreableLandName, cfg => cfg.MapFrom(c => c.ArableLand.Name))
                .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
                .ForMember(x => x.SizeInDecar, cfg => cfg.MapFrom(c => c.ArableLand.SizeInDecar))
                .ForMember(x => x.SeasonName, cfg => cfg.MapFrom(c => c.WorkingSeason.Name))
                .ForMember(x => x.StartDate, cfg => cfg.MapFrom(c => c.WorkingSeason.StartDate))
                .ForMember(x => x.EndDate, cfg => cfg.MapFrom(c => c.WorkingSeason.EndDate));
    }
}
