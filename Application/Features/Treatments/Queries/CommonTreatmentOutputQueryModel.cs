namespace Application.Features.Treatments.Queries
{
    public class CommonTreatmentOutputQueryModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal AmountOfFuel { get; set; }

        public decimal FuelPrice { get; set; }

        public int ArticleId { get; set; }

        public string ArticleName { get; set; } = default!;

        public decimal ArticleQuantity { get; set; }

        public decimal ArticlePrice { get; set; }
    }
}
