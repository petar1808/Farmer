namespace Application.Features.PerformedWorks.Queries
{
    public class CommonPerformedWorkOutputQueryModel
    {
        public int Id { get; init; }

        public DateTime Date { get; init; }

        public decimal AmountOfFuel { get; init; }

        public decimal FuelPrice { get; init; }
    }
}
