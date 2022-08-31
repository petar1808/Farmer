namespace WebUI.ServicesModel.Seeding
{
    public class GetSeedingModel
    {
        public int? ArticleId { get; set; }

        public string ArticleName { get; set; } = default!;

        public int SownSeedsInTotal { get; set; }

        public int SeedPricePerKilogram { get; set; }

        public int PriceOfSeedsInTotal { get; set; }

        public int HarvestedQuantityPerDecare { get; set; }

        public int TotalAmountInKilogram { get; set; }

        public int GrainPricePerKilogram { get; set; }

        public int PriceOfGrainTotal { get; set; }

        public int Subsidies { get; set; }

        public int Income { get; set; }

        public int Profit { get; set; }
    }
}
