namespace WebUI.ServicesModel.Subsidies
{
    public class SubsidiesBaseModel
    {
        public int Id { get; set; }

        public decimal? Income { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
