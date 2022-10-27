namespace WebUI.ServicesModel.Seeding
{
    public class GetSeedingSummaryModel : SeedingSummaryBaseModel
    {
        public string ArticleName { get; set; } = default!;

        public decimal IncomeFromHarvestedGrains { get; set; }

        public decimal Expenses { get; set; }

        public decimal Profit { get; set; }

        public decimal Income { get; set; }
    }
}
