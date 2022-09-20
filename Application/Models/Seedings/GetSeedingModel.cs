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
    public class GetSeedingModel : IMapFrom<Seeding>
    {
        public int? ArticleId { get; init; }

        public string ArticleName { get; init; } = default!;

        public int SeedsQuantityPerDecare { get; private set; } 

        public decimal SeedsPricePerKilogram { get; private set; } 

        public int HarvestedQuantityPerDecare { get; private set; }

        public decimal HarvestedGrainSellingPricePerKilogram { get; private set; }

        public decimal SubsidiesIncome { get; private set; }

        public decimal IncomeFromHarvestedGrains { get; private set; }

        public decimal Expenses { get; private set; }

        public decimal Profit => (IncomeFromHarvestedGrains + SubsidiesIncome) - Expenses;

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, GetSeedingModel>()
                .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
                .ForMember(x => x.ArticleId, cfg => cfg.MapFrom(c => c.Article.Id))
                .ForMember(x => x.IncomeFromHarvestedGrains, cfg => cfg.MapFrom(c => c.HarvestedQuantityPerDecare * c.HarvestedGrainSellingPricePerKilogram))
                .ForMember(x => x.Expenses, cfg => cfg.MapFrom(c => c.SeedsQuantityPerDecare * c.SeedsPricePerKilogram));
    }
}
