using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Тreatments
{
    public class AddТreatmentModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; init; }

        [Required]
        [EnumDataType(typeof(ТreatmentType))]
        public ТreatmentType TreatmentType { get; init; }

        public int? AmountOfFuel { get; init; }

        public int? FuelPrice { get; init; }

        [Required]
        public int ArticleId { get; init; }

        [Required]
        public int ArticleQuantity { get; init; }

        [Required]
        public int ArticlePrice { get; init; }
    }
}
