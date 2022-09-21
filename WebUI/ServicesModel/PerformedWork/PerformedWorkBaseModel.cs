namespace WebUI.ServicesModel.PerformedWork
{
    public class PerformedWorkBaseModel
    {
        public int Id { get; set; }

        public int SeedingId { get; set; }

        public DateTime Date { get; set; }

        public int AmountOfFuel { get; set; }

        public int FuelPrice { get; set; }
    }
}
