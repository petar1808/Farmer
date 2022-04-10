using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Seeding : Entity<int>
    {

        public Seeding(int arableLandId,
            int workingSeasonId,
            int articleId,
            int quantityOfGrainSowing,
            int quantityGrainHarvest)
            : this(arableLandId, workingSeasonId, articleId)
        {
            QuantityGrainHarvest = quantityGrainHarvest;
            QuantityOfGrainSowing = quantityOfGrainSowing;
        }

        public Seeding(int arableLandId,
             int workingSeasonId,
             int articleId)
        {
            ArableLandId = arableLandId;
            WorkingSeasonId = workingSeasonId;
            ArticleId = articleId;

            ArableLand = default!;
            WorkingSeason = default!;
            Article = default!;
        }

        public int ArableLandId { get;}

        public ArableLand ArableLand { get; set; }

        public int WorkingSeasonId { get; }

        public WorkingSeason WorkingSeason { get;}

        public int ArticleId { get;}

        public Article Article { get;}

        public int QuantityOfGrainSowing { get; } = default!;

        public int QuantityGrainHarvest { get; } = default!;
    }
}
