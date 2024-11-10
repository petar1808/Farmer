namespace Application.Features.Seedings.Commands.Edit
{
    public class EditArableLandBalanceInputCommandModel
    {
        public int SeedingId { get; private set; }

        public int? ArticleId { get; set; }

        public decimal SeedsQuantityPerDecare { get; set; }

        public int HarvestedQuantityPerDecare { get; set; }

        public decimal HarvestedGrainSellingPricePerKilogram { get; set; }

        public void SetSeedingId(int seedingId)
        {
            SeedingId = seedingId;
        }
    }
}
