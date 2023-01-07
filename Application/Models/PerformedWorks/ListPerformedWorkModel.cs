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

namespace Application.Models.PerformedWorks
{
    public class ListPerformedWorkModel : IMapFrom<PerformedWork>
    {
        public int Id { get; init; }

        public string WorkType { get; init; } = default!;

        public DateTime Date { get; init; }

        public decimal AmountOfFuel { get; init; }

        public decimal FuelPrice { get; init; }

        public decimal FuelPriceTotal => AmountOfFuel * FuelPrice;

        public virtual void Mapping(Profile mapper)
        => mapper.CreateMap<PerformedWork, ListPerformedWorkModel>()
            .ForMember(x => x.WorkType, cfg => cfg.MapFrom(c => c.WorkType.GetEnumDisplayName()));
    }
}
