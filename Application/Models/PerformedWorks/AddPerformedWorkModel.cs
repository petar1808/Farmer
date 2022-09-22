using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.PerformedWorks
{
    public class AddPerformedWorkModel
    {
        public WorkType WorkType { get; init; }

        public DateTime Date { get; init; }

        public int AmountOfFuel { get; init; }

        public int FuelPrice { get; init; }
    }
}
