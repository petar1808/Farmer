using Application.Mappings;
using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Тreatments
{
    public class GetTreatmentModel : IMapFrom<Treatment>
    {
        public int Id { get; set; }

        public DateTime Date { get; init; }

        public ТreatmentType ТreatmentType { get; init; } = default!;

        public int? AmountOfFuel { get; init; }

        public int? FuelPrice { get; init; }

        public int ArticleId { get; init; }

        public int ArticleQuantity { get; init; }

        public int ArticlePrice { get; init; }
    }
}
