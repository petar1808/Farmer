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

        [EnumDataType(typeof(WorkType))]
        public WorkType WorkType { get; init; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; init; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AmountOfFuel { get; init; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal FuelPrice { get; init; }
    }
}
