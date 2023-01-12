using Domain.Common;

namespace Domain.Models
{
    public class Subsidy : Entity<int>, ITenant
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

        public int TenantId { get; set; }

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
