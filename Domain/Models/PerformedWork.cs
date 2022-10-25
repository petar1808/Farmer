using Domain.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PerformedWork : Entity<int>
    {

        public PerformedWork(int seedingId,
            WorkType workType,
            DateTime date,
            decimal fuelPrice,
            int amountOfFuel)
        {
            Validate(workType);
            SeedingId = seedingId;
            WorkType = workType;
            Date = date;
            FuelPrice = fuelPrice;
            AmountOfFuel = amountOfFuel;

            Seeding = default!;
        }

        public int SeedingId { get;}

        public Seeding Seeding { get;}

        public WorkType WorkType { get; private set; }

        public DateTime Date { get; private set; }

        public int AmountOfFuel { get; private set; } 

        public decimal FuelPrice { get; private set; }

        public PerformedWork UpdateWorkType(WorkType workType)
        {
            ValidateWorkType(workType);
            this.WorkType = workType;
            return this;
        }

        public PerformedWork UpdateDate(DateTime date)
        {
            this.Date = date;
            return this;
        }

        public PerformedWork UpdateAmountOfFuel(int amountOfFuel)
        {
            this.AmountOfFuel = amountOfFuel;
            return this;
        }

        public PerformedWork UpdateFuelPrice(int fuelPrice)
        {
            this.FuelPrice = fuelPrice;
            return this;
        }

        private void ValidateWorkType(WorkType type)
            => Guard.Guard.ForValidEnum<WorkType>((int)type, nameof(WorkType));

        private void Validate(WorkType type)
        {
            ValidateWorkType(type);
        }
    }
}
