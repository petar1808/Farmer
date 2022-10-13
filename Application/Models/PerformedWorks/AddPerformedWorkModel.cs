using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.PerformedWorks
{
    public class AddPerformedWorkModel
    {
        [Required]
        [EnumDataType(typeof(WorkType))]
        public WorkType WorkType { get; init; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; init; }

        [Required]
        public int AmountOfFuel { get; init; }

        [Required]
        public int FuelPrice { get; init; }
    }
}
