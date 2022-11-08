namespace WebUI.ServicesModel.Seeding
{
    public class GetSeedingSummaryModel : SeedingSummaryBaseModel
    {
        public string ArticleName { get; set; } = default!;

        public decimal IncomeFromHarvestedGrains { get; set; }
    }
}
