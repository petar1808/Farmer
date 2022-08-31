namespace WebUI.ServicesModel.Тreatment
{
    public class ТreatmentBaseModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int? AmountOfFuel { get; set; }

        public int? FuelPrice { get; set; }

        public int ArticleId { get; set; }

        public int ArticleQuantity { get; set; }

        public int ArticlePrice { get; set; }
    }
}
