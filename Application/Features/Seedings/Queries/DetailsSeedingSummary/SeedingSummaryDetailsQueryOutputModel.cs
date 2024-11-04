using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Seedings.Queries.DetailsSeedingSummary
{
    public class SeedingSummaryDetailsQueryOutputModel : IMapFrom<Seeding>
    {
        public int Id { get; set; }

        public int? ArticleId { get; set; }

        public decimal SeedsQuantityPerDecare { get; set; }

        public int HarvestedQuantityPerDecare { get; set; }

        public decimal HarvestedGrainSellingPricePerKilogram { get; set; }

        public string ArticleName { get; set; } = default!;

        public decimal IncomeFromHarvestedGrains { get; set; }

        public int ArableLandSize { get; set; }

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, SeedingSummaryDetailsQueryOutputModel>()
                .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
                .ForMember(x => x.ArticleId, cfg => cfg.MapFrom(c => c.Article.Id))
                .ForMember(x => x.ArableLandSize, cfg => cfg.MapFrom(c => c.ArableLand.SizeInDecar))
                .ForMember(x => x.IncomeFromHarvestedGrains, cfg => cfg.MapFrom(c => (c.HarvestedQuantityPerDecare * c.HarvestedGrainSellingPricePerKilogram) * c.ArableLand.SizeInDecar));
    }
}
