using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Subsidies
{
    public class SubsidyBaseModel
    {
        [Range(0d, int.MaxValue, ErrorMessage = "Прихода трябва да е положително число")]
        public decimal Income { get; init; }

        [Required(ErrorMessage = "Датата е задължителна")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; init; }
    }
}
