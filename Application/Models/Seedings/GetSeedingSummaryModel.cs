﻿using Application.Mappings;
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

        public decimal TotalSeedCost { get; private set; }

        public virtual void Mapping(Profile mapper)
           => mapper.CreateMap<Seeding, GetSeedingSummaryModel>()
                .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
                .ForMember(x => x.ArticleId, cfg => cfg.MapFrom(c => c.Article.Id))
                .ForMember(x => x.IncomeFromHarvestedGrains, cfg => cfg.MapFrom(c => (c.HarvestedQuantityPerDecare * c.HarvestedGrainSellingPricePerKilogram) * c.ArableLand.SizeInDecar))
                .ForMember(x => x.TotalSeedCost, cfg => cfg.MapFrom(c => c.ArableLand.SizeInDecar * (c.SeedsPricePerKilogram * c.SeedsQuantityPerDecare)));
    }
}
