namespace WebUI.ServicesModel.Subsidies
{
    public class SubsidyBaseModel
    {
        public int Id { get; set; }

        public decimal? Income { get; set; }

        public string Comment { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
