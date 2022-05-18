namespace Web.ViewModels.PerformedWork
{
    public class PerformedWorkListViewModel
    {
        public DateTime PerfomedWorkDate { get; init; } = default!;

        public string PerfomedWorkType { get; init; } = default!;

        public int FuelUsed { get; init; }

        public int FuelSum { get; init; }
    }
}
