namespace Web.ViewModels.WorkingSeasons
{
    public class WorkingSeasonListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
