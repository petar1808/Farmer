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

        public int SownSeedsInTotal { get; init; }

        public int SeedPricePerKilogram { get; init; }

        public int PriceOfSeedsInTotal { get; init; }

        public int HarvestedQuantityPerDecare { get; init; }

        public int TotalAmountInKilogram { get; init; }

        public int GrainPricePerKilogram { get; init; }

        public int PriceOfGrainTotal { get; init; }

        public int Subsidies { get; init; }

        public int Income { get; init; }

        public int Profit { get; init; }

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, GetSeedingModel>()
                .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
                .ForMember(x => x.ArticleId, cfg => cfg.MapFrom(c => c.Article.Id));
    }
}
