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
            int articleId,
            DateTime performedWorkDate,
            int fuelSum,
            int fuelUsed)
            :this(seedingId, workType, performedWorkDate, fuelSum, fuelUsed)
        {
            FuelUsed = fuelUsed;
            FuelSum = fuelSum;
            ArticleId = articleId;
        }

        public PerformedWork(int seedingId,
            WorkType workType,
            DateTime performedWorkDate,
            int fuelSum,
            int fuelUsed)
        {
            SeedingId = seedingId;
            WorkType = workType;
            PerforemedWorkDate = performedWorkDate;
            FuelSum = fuelSum;
            FuelUsed = fuelUsed;

            Seeding = default!;
            Article = default!;
        }

        private PerformedWork()
        {
            Seeding = default!;
            Article = default!;
        }

        public int SeedingId { get;}

        public Seeding Seeding { get;}

        public WorkType WorkType { get;}
        
        public int? ArticleId { get;}

        public Article Article { get;}

        public DateTime PerforemedWorkDate { get; } = default!;

        public int FuelUsed { get; } = default!;

        public int FuelSum { get; }
    }
}
