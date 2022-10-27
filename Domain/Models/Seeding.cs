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
             int workingSeasonId)
        {
            ArableLandId = arableLandId;
            WorkingSeasonId = workingSeasonId;

            ArableLand = default!;
            WorkingSeason = default!;
            Article = default!;
        }

        public int ArableLandId { get; private set; }

        public ArableLand ArableLand { get; private set; }

        public int WorkingSeasonId { get;  }

        public WorkingSeason WorkingSeason { get; }

        public int? ArticleId { get; private set; }

        public Article Article { get; private set; }

        public int SeedsQuantityPerDecare { get; private set; }

        public decimal SeedsPricePerKilogram { get; private set; }

        public int HarvestedQuantityPerDecare { get; private set; }

        public decimal HarvestedGrainSellingPricePerKilogram { get; private set; }

        public decimal SubsidiesIncome { get; private set; }

        public decimal ExpensesForHarvesting { get; private set; }

        public List<Treatment> Treatments { get; } = default!;

        public List<PerformedWork> PerformedWorks { get; } = default!;

        public Seeding UpdateArticle(int? articleId)
        {
            this.ArticleId = articleId;
            return this;
        }

        public Seeding UpdateExpensesForHarvesting(decimal expensesForHarvesting)
        {
            this.ExpensesForHarvesting = expensesForHarvesting;
            return this;
        }

        public Seeding UpdateSeedsQuantityPerDecare(int seedsQuantityPerDecare)
        {
            this.SeedsQuantityPerDecare = seedsQuantityPerDecare;
            return this;
        }

        public Seeding UpdateSeedsPricePerKilogram(decimal seedsPricePerKilogram)
        {
            this.SeedsPricePerKilogram = seedsPricePerKilogram;
            return this;
        }

        public Seeding UpdateHarvestedQuantityPerDecare(int harvestedQuantityPerDecare)
        {
            this.HarvestedQuantityPerDecare = harvestedQuantityPerDecare;
            return this;
        }

        public Seeding UpdateHarvestedGrainSellingPricePerKilogram(decimal harvestedGrainSellingPricePerKilogram)
        {
            this.HarvestedGrainSellingPricePerKilogram = harvestedGrainSellingPricePerKilogram;
            return this;
        }

        public Seeding UpdateSubsidies(decimal subsidiesIncome)
        {
            this.SubsidiesIncome = subsidiesIncome;
            return this;
        }
    }
}
