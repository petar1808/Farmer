using Application.Mappings;
using Domain.Enum;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.PerformedWorks
{
    public class GetPerformedWorkModel : IMapFrom<PerformedWork>
    {
        public int Id { get; init; }

        public WorkType WorkType { get; init; } = default!;

        public DateTime Date { get; init; }

        public int AmountOfFuel { get; init; }

        public decimal FuelPrice { get; init; }
    }
}
