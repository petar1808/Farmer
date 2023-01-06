using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Subsidy : Entity<int>
    {
        public Subsidy(int seedingId, decimal income, DateTime date)
        {
            SeedingId = seedingId;
            Income = income;
            Date = date;
            Seeding = default!;
        }

        public int SeedingId { get; }

        public Seeding Seeding { get; }

        public decimal Income { get; private set; }

        public DateTime Date { get; private set; }

        public Subsidy UpdateIncome(decimal income)
        {
            this.Income = income;
            return this;
        }

        public Subsidy UpdateDate(DateTime date)
        {
            this.Date = date;
            return this;
        }
    }
}
