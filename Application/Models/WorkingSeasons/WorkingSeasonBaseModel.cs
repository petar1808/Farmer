using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.ModelConstraint.CommonConstraints;

namespace Application.Models.WorkingSeasons
{
    public class WorkingSeasonBaseModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        public string Name { get; init; } = default!;

        [Required(ErrorMessage = "Началната дата е задължителна")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; init; }

        [Required(ErrorMessage = "Крайната дата е задължителна")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; init; }
    }
}
