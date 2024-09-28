using Domain.Common;
using System.Xml.Linq;

namespace Domain.Models
{
    public class Subsidy : Entity<int>, ITenant
    {
        public Subsidy(
            decimal income,
            int workingSeasonId,
            DateTime date,
            List<SubsidyByArableLand> subsidyByArableLands,
            string comment = "")
        {
            Income = income;
            Date = date;
            SubsidyByArableLands = subsidyByArableLands;
            WorkingSeasonId = workingSeasonId;
            Comment = comment;
        }

        private Subsidy()
        {
            Income = default!;
            Date = default!;
            SubsidyByArableLands = default!;
            WorkingSeasonId = default!;
            Comment = default!;
        }

        public decimal Income { get; private set; }

        public DateTime Date { get; private set; }

        public int WorkingSeasonId { get; set; }

        public int TenantId { get; set; }

        public string Comment { get; set; }

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

        public Subsidy UpdateComment(string comment)
        {
            this.Comment = comment;
            return this;
        }
    }
}
