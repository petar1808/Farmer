namespace Web.Models.Seedings
{
    public class AddSeedingsModel
    {
        public int ArableLandId { get; }

        public int WorkingSeasonId { get; }

        public int ArticleId { get; }

        public int QuantityOfGrainSowing { get; } = default!;

        public int QuantityGrainHarvest { get; } = default!;
    }
}
