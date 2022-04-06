using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class WorkingSeason : Entity<int>
    {
        public WorkingSeason(string name,
            DateTime? startDate,
            DateTime? endDate)
            :this(name)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public WorkingSeason(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public DateTime? StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public WorkingSeason UpdateName(string name)
        {
            this.Name = name;
            return this;
        }

        public WorkingSeason UpdateSratDate(DateTime? sratDate)
        {
            this.StartDate = sratDate;
            return this;
        }

        public WorkingSeason UpdateEndDate(DateTime? endDate)
        {
            this.StartDate = endDate;
            return this;
        }
    }
}
