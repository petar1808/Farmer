using Domain.Enum;

namespace Web.ViewModels.PerformedWork
{
    public class AddBaseModel
    {
        public DateTime PerformedWorkDate { get; init; } = default!;

        public WorkType Type { get; init; }

        public int FuelUsed { get; set; }

        public int FuelSum { get; set; }

        public int SeedingId { get; set; }
    }
}
