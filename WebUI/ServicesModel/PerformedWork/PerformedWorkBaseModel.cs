namespace WebUI.ServicesModel.PerformedWork
{
    public class PerformedWorkBaseModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int AmountOfFuel { get; set; }

        public int FuelPrice { get; set; }
    }
}
