namespace WebUI.ServicesModel.Тreatment
{
    public class ТreatmentBaseModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public decimal AmountOfFuel { get; set; }

        public decimal FuelPrice { get; set; }

        public int ArticleId { get; set; }

        public decimal ArticleQuantity { get; set; }

        public decimal ArticlePrice { get; set; }
    }
}
