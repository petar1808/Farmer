using Domain.Common;
using static Domain.ModelConstraint.WorkingSeasonConstraints;

namespace Domain.Models
{
    public class WorkingSeason : Entity<int>
    {
        public WorkingSeason(string name,
            DateTime startDate,
            DateTime endDate)
            : this(name)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public WorkingSeason(string name)
        {
            Validate(name);
            Name = name;
            Seedings = default!;
        }

        public string Name { get; private set; }

        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        public List<Seeding> Seedings { get; private set; }

        public WorkingSeason UpdateName(string name)
        {
            ValidateName(name);
            this.Name = name;
            return this;
        }

        public WorkingSeason UpdateSratDate(DateTime sratDate)
        {
            this.StartDate = sratDate;
            return this;
        }

        public WorkingSeason UpdateEndDate(DateTime endDate)
        {
            this.EndDate = endDate;
            return this;
        }

        private void ValidateName(string name)
            => Guard.Guard.ForStringMaxLengtAndMinLength(
          name,
          MaxLenghtName,
          MinLenghtName,
          nameof(this.Name));

        private void Validate(string name)
        {
            ValidateName(name);
        }
    }
}
