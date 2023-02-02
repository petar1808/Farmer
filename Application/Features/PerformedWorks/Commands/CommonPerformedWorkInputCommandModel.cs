using Domain.Enum;

namespace Application.Features.PerformedWorks.Commands
{
    public class CommonPerformedWorkInputCommandModel
    {
        public WorkType WorkType { get; init; }

        public DateTime Date { get; init; }

        public decimal AmountOfFuel { get; init; }

        public decimal FuelPrice { get; init; }
    }
}
