using Domain.Common;

namespace Domain.Models
{
    public class Subsidy : Entity<int>, ITenant
    {
        public Subsidy(
            decimal income,
            int workingSeasonId,
            DateTime date,
            List<SubsidyByArableLand> subsidyByArableLands)
        {
            Income = income;
            Date = date;
            SubsidyByArableLands = subsidyByArableLands;
            WorkingSeasonId = workingSeasonId;
        }

        public Subsidy()
        {
            Income = default!;
            Date = default!;
            SubsidyByArableLands = default!;
            WorkingSeasonId = default!;
        }

        public decimal Income { get; private set; }

        public DateTime Date { get; private set; }

        public int WorkingSeasonId { get; set; }

        public int TenantId { get; set; }

        public List<SubsidyByArableLand> SubsidyByArableLands { get; set; }

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
