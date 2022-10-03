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

        public int? AmountOfFuel { get; init; }

        public int? FuelPrice { get; init; }

        public int ArticleId { get; init; }

        public int ArticleQuantity { get; init; }

        public int ArticlePrice { get; init; }

        public virtual void Mapping(Profile mapper)
        => mapper.CreateMap<Treatment, ListТreatmentModel>()
            .ForMember(x => x.TreatmentType, cfg => cfg.MapFrom(c => c.TreatmentType.GetEnumDisplayName()));
    }
}
