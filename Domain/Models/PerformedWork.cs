using Domain.Common;
using Domain.Enum;

namespace Domain.Models
{
    public class PerformedWork : Entity<int>, ITenant
    {

        public PerformedWork(int seedingId,
            WorkType workType,
            DateTime date)
        {
            ValidateWorkType(workType);
            SeedingId = seedingId;
            WorkType = workType;
            Date = date;
            Seeding = default!;
        }


        public int SeedingId { get; }

        public Seeding Seeding { get; }

        public WorkType WorkType { get; private set; }

        public DateTime Date { get; private set; }

        public int TenantId { get; set; }

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

        private void ValidateWorkType(WorkType type)
            => Guard.Guard.ForValidEnum<WorkType>((int)type, nameof(WorkType));
    }
}
