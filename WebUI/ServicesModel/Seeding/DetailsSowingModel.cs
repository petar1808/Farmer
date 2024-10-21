namespace WebUI.ServicesModel.Seeding
{
    public class DetailsSowingModel
    {
        public int ArticleId { get; set; }

        // Количество семена на декар
        public decimal SeedsQuantityPerDecare { get; set; } = default!;

        // Общо количество семена
        public decimal SumSeedsQuantity
        {
            get
            {
                return ArableLandSize * SeedsQuantityPerDecare;
            }
        }

        // Ожънато количестоп на декар
        public int HarvestedQuantityPerDecare { get; set; }

        // Сума ожънато количество
        public decimal SumHarvestedQuantity
        {
            get
            {
                return ArableLandSize * HarvestedQuantityPerDecare;
            }
        }

        // Продажна цена реколта на кг
        public decimal HarvestedGrainSellingPricePerKilogram { get; set; }

        // Приход от реколта
        public decimal IncomeFromHarvestedGrains
        {
            get
            {
                return SumHarvestedQuantity * HarvestedGrainSellingPricePerKilogram;
            }
        }

        public int ArableLandSize { get; set; }
    }
}
