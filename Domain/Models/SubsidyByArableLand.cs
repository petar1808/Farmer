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
        }
        public int ArableLandId { get; set; }

        public ArableLand ArableLand { get; set; }

        public decimal Income { get; private set; }

        public int SubsidyId { get; set; }

        public int TenantId { get; set; }
    }
}
