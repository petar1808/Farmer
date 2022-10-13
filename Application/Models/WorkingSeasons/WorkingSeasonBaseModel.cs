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
        [Required]
        [MaxLength(MaxNameLenght)]
        public string Name { get; init; } = default!;

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; init; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; init; }
    }
}
