namespace WebUI.ServicesModel.Seeding
{
    public class ListSeedingModel
    {
        public int Id { get; set; }

        public string ArableLandName { get; set; } = string.Empty;

        public string ArticleName { get; set; } = string.Empty;

        public decimal SeedsQuantity { get; set; }

        public int HarvestedQuantity { get; set; }

        public decimal Income { get; set; }
    }
}
