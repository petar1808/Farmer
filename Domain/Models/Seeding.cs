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
            int? articleId,
            int quantityOfSeedsPerDecare,
            int seedPricePerKilogram,
            int grainPricePerKilogram,
            int harvestedQuantityPerDecare,
            int subsidies)
            : this(arableLandId, workingSeasonId)
        {
            ArticleId = articleId;
            SeedPricePerKilogram = seedPricePerKilogram;
            QuantityOfSeedsPerDecare = quantityOfSeedsPerDecare;
            GrainPricePerKilogram = grainPricePerKilogram;
            HarvestedQuantityPerDecare = harvestedQuantityPerDecare;
            Subsidies = subsidies;
        }

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

        public ArableLand ArableLand { get; set; }

        public int WorkingSeasonId { get; private set; }

        public WorkingSeason WorkingSeason { get; }

        public int? ArticleId { get; private set; }

        public Article Article { get; set; }

        public int QuantityOfSeedsPerDecare { get; private set; } = default!;

        public int SeedPricePerKilogram { get; private set; } = default!;

        public int GrainPricePerKilogram { get; private set; }

        public int HarvestedQuantityPerDecare { get; private set; }

        public int Subsidies { get; private set; }

        public Seeding UpdateArableLand(int arableLandId)
        {
            this.ArableLandId = arableLandId;
            return this;
        }

        public Seeding UpdateArticle(int articleId)
        {
            this.ArticleId = articleId;
            return this;
        }

        public Seeding UpdateWorkingSeason(int workingSeasonId)
        {
            this.WorkingSeasonId = workingSeasonId;
            return this;
        }

        public Seeding UpdateQuantityOfSeedsPerDecare(int quantityOfSeedsPerDecare)
        {
            this.QuantityOfSeedsPerDecare = quantityOfSeedsPerDecare;
            return this;
        }

        public Seeding UpdateSeedPricePerKilogram(int seedPricePerKilogram)
        {
            this.SeedPricePerKilogram = seedPricePerKilogram;
            return this;
        }

        public Seeding UpdateGrainPricePerKilogram(int grainPricePerKilogram)
        {
            this.GrainPricePerKilogram = grainPricePerKilogram;
            return this;
        }

        public Seeding UpdateHarvestedQuantityPerDecare(int harvestedQuantityPerDecare)
        {
            this.HarvestedQuantityPerDecare = harvestedQuantityPerDecare;
            return this;
        }

        public Seeding UpdateSubsidies(int subsidies)
        {
            this.Subsidies = subsidies;
            return this;
        }
    }
}
