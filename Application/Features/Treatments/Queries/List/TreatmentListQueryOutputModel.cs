using Application.Extensions;
using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.Treatments.Queries.List
{
    public class TreatmentListQueryOutputModel : CommonTreatmentOutputQueryModel, IMapFrom<Treatment>
    {
        public string TreatmentType { get; set; } = default!;

        public decimal ArticlePriceTotal { get; set; }

        public virtual void Mapping(Profile mapper)
        => mapper.CreateMap<Treatment, TreatmentListQueryOutputModel>()
            .ForMember(x => x.TreatmentType, cfg => cfg.MapFrom(c => c.TreatmentType.GetEnumDisplayName()))
            .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name))
            .ForMember(x => x.ArticlePriceTotal, cfg => cfg.MapFrom(c => c.Seeding.ArableLand.SizeInDecar * (c.ArticlePrice * c.ArticleQuantity)));
    }
}
