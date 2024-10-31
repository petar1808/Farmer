using Domain.Common;

namespace Domain.Models
{
    public class SubsidyByArableLand : Entity<int>, ITenant
    {
        public SubsidyByArableLand(int arableLandId, decimal income)
        {
            ArableLandId = arableLandId;
            Income = income;
            ArableLand = default!;
            Subsidy = default!;
        }
        public int ArableLandId { get; set; }

        public ArableLand ArableLand { get; set; }

        public decimal Income { get; private set; }

        public int SubsidyId { get; set; }

        public Subsidy Subsidy { get; set; }

        public int TenantId { get; set; }

        public SubsidyByArableLand UpdateIncome(decimal income)
        {
            Income = income;
            return this;
        }
    }
}
