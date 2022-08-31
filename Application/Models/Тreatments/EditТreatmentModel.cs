using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Тreatments
{
    public class EditТreatmentModel
    {
        public int Id { get; init; }

        public DateTime Date { get; init; }

        public ТreatmentType ТreatmentType { get; init; }

        public int? AmountOfFuel { get; init; }

        public int? FuelPrice { get; init; }

        public int ArticleId { get; init; }

        public int ArticleQuantity { get; init; }

        public int ArticlePrice { get; init; }
    }
}
