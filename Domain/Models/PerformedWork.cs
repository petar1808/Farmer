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
            int fuelUsed)
            :this(seedingId,workType,articleId)
        {
            FuelUsed = fuelUsed;
        }

        public PerformedWork(int seedingId,
            WorkType workType,
            int articleId)
        {
            SeedingId = seedingId;
            WorkType = workType;
            ArticleId = articleId;

            Seeding = default!;
            Article = default!;
        }

        public int SeedingId { get;}

        public Seeding Seeding { get;}

        public WorkType WorkType { get;}

        public int ArticleId { get;}

        public Article Article { get;}

        public int FuelUsed { get; } = default!;
    }
}
