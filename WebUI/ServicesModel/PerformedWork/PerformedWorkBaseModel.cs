namespace WebUI.ServicesModel.PerformedWork
{
    public class PerformedWorkBaseModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public decimal? AmountOfFuel { get; set; }

        public decimal? FuelPrice { get; set; }
    }
}
