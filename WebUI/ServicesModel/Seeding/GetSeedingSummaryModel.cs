namespace WebUI.ServicesModel.Seeding
{
    public class GetSeedingSummaryModel : SeedingSummaryBaseModel
    {
        public string ArticleName { get; set; } = default!;

        public decimal IncomeFromHarvestedGrains => HarvestedQuantityPerDecare * HarvestedGrainSellingPricePerKilogram;

        public decimal Expenses => SeedsQuantityPerDecare * SeedsPricePerKilogram;

        public decimal Profit => (IncomeFromHarvestedGrains + SubsidiesIncome) - Expenses;
    }
}
