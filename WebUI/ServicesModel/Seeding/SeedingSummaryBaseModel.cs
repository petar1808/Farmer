namespace WebUI.ServicesModel.Seeding
{
    public class SeedingSummaryBaseModel
    {
        public int? ArticleId { get; set; }

        public decimal SeedsQuantityPerDecare { get; set; } = default!;

        public decimal SeedsPricePerKilogram { get; set; } = default!;

        public int HarvestedQuantityPerDecare { get; set; }

        public decimal HarvestedGrainSellingPricePerKilogram { get; set; }

        public decimal ExpensesForHarvesting { get; set; }
    }
}
