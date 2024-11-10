namespace WebUI.ServicesModel.Тreatment
{
    public class ТreatmentBaseModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int ArticleId { get; set; }

        public decimal? ArticleQuantity { get; set; }
    }
}
