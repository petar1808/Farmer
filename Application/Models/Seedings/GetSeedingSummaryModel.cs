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
    public class GetSeedingSummaryModel : SeedingSummaryBaseModel, IMapFrom<Seeding>
    {
        public string ArticleName { get; init; } = default!;

        public decimal IncomeFromHarvestedGrains { get; private set; }

        public decimal Expenses { get; private set; }

        public decimal Profit => (IncomeFromHarvestedGrains + SubsidiesIncome) - Expenses;

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, GetSeedingSummaryModel>()
                .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
                .ForMember(x => x.ArticleId, cfg => cfg.MapFrom(c => c.Article.Id))
                .ForMember(x => x.IncomeFromHarvestedGrains, cfg => cfg.MapFrom(c => c.HarvestedQuantityPerDecare * c.HarvestedGrainSellingPricePerKilogram))
                .ForMember(x => x.Expenses, cfg => cfg.MapFrom(c => c.SeedsQuantityPerDecare * c.SeedsPricePerKilogram));
    }
}
