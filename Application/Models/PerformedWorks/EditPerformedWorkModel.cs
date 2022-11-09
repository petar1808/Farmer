using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.PerformedWorks
{
    public class EditPerformedWorkModel
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "Типът работа е задължителен")]
        [EnumDataType(typeof(WorkType))]
        public WorkType WorkType { get; init; }

        [Required(ErrorMessage = "Датата е задължителна")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Количеството гориво е задължително")]
        [Range(1, int.MaxValue, ErrorMessage = "Количеството гориво трябва да положително число")]
        public int AmountOfFuel { get; init; }

        [Required(ErrorMessage = "Цената на горивото е задължителна")]
        [Range(0d, int.MaxValue, ErrorMessage = "Цената на горивото трябва да положително число")]
        public decimal FuelPrice { get; init; }
    }
}
