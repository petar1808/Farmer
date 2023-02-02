using Application.Extensions;
using Application.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Features.PerformedWorks.Queries.List
{
    public class PerformedWorkListQueryOutputModel : CommonPerformedWorkOutputQueryModel, IMapFrom<PerformedWork>
    {
        public string WorkType { get; init; } = default!;

        public decimal FuelPriceTotal => AmountOfFuel * FuelPrice;

        public virtual void Mapping(Profile mapper)
        => mapper.CreateMap<PerformedWork, PerformedWorkListQueryOutputModel>()
            .ForMember(x => x.WorkType, cfg => cfg.MapFrom(c => c.WorkType.GetEnumDisplayName()));
    }
}
