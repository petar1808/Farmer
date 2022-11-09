using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Тreatments
{
    public class EditТreatmentModel
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "Датата е задължителна")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Типът третиране е задължителен")]
        [EnumDataType(typeof(ТreatmentType))]
        public ТreatmentType TreatmentType { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "Количеството гориво трябва да е положително число")]
        public int? AmountOfFuel { get; init; }

        [Range(0, int.MaxValue, ErrorMessage = "Цената на горивото трябва да е положително число")]
        public decimal? FuelPrice { get; init; }

        [Required]
        public int ArticleId { get; init; }

        [Required(ErrorMessage = "Количеството на артикула е задължително")]
        [Range(1, int.MaxValue, ErrorMessage = "Количеството на артикула трябва да е положително число")]
        public int ArticleQuantity { get; init; }

        [Required(ErrorMessage = "Цената на артикула е задължителна")]
        [Range(0, int.MaxValue, ErrorMessage = "Цената на артикула трябва да е положително число")]
        public decimal ArticlePrice { get; init; }
    }
}
