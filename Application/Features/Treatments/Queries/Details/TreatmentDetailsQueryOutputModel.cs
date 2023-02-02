using Application.Mappings;
using AutoMapper;
using Domain.Enum;
using Domain.Models;

namespace Application.Features.Treatments.Queries.Details
{
    public class TreatmentDetailsQueryOutputModel : CommonTreatmentOutputQueryModel, IMapFrom<Treatment>
    {
        public ТreatmentType TreatmentType { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<Treatment, TreatmentDetailsQueryOutputModel>()
               .ForMember(x => x.ArticleName, cfg => cfg.MapFrom(c => c.Article.Name));
    }
}
