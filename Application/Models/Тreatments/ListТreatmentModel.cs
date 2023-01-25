using Application.Extensions;
using Application.Mappings;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Тreatments
{
    public class ListТreatmentModel : IMapFrom<Treatment>
    {
        public int Id { get; set; }

        public DateTime Date { get; init; }

        public string TreatmentType { get; init; } = default!;

        public decimal AmountOfFuel { get; init; }

        public decimal FuelPrice { get; init; }

        public int ArticleId { get; init; }

        public string ArticleName { get; set; } = default!;

        public decimal ArticleQuantity { get; init; }

        public decimal ArticlePrice { get; init; }

        public decimal ArticlePriceTotal { get; init; }

        public virtual void Mapping(Profile mapper)
        => mapper.CreateMap<Treatment, ListТreatmentModel>()
            .ForMember(x => x.TreatmentType, cfg => cfg.MapFrom(c => c.TreatmentType.GetEnumDisplayName()))
            .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
            .ForMember(x => x.ArticlePriceTotal, cfg => cfg.MapFrom(c => c.Seeding.ArableLand.SizeInDecar * (c.ArticlePrice * c.ArticleQuantity)));
    }
}
