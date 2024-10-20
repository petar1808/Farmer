using Application.Features.Seedings.Queries.DetailsSeedingSummary;
using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Seedings.Queries.ListSeeding
{
    public class ListSeedingQueryOutputModel : IMapFrom<Seeding>
    {
        public int Id { get; set; }

        public string ArableLandName { get; set; } = string.Empty;

        public string ArticleName { get; set; } = string.Empty;

        public decimal SeedsQuantity { get; set; }

        public int HarvestedQuantity { get; set; }

        public decimal Income { get; set; }

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<Seeding, ListSeedingQueryOutputModel>()
               .ForMember(x => x.ArableLandName, cfg => cfg.MapFrom(c => c.ArableLand.Name))
               .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
               .ForMember(x => x.SeedsQuantity, cfg => cfg.MapFrom(c => c.SeedsQuantityPerDecare * c.ArableLand.SizeInDecar))
               .ForMember(x => x.HarvestedQuantity, cfg => cfg.MapFrom(c => c.HarvestedQuantityPerDecare * c.ArableLand.SizeInDecar))
               .ForMember(x => x.Income, cfg => cfg.MapFrom(c => c.ArableLand.SizeInDecar * (c.SeedsPricePerKilogram * c.SeedsQuantityPerDecare)));
    }
}
