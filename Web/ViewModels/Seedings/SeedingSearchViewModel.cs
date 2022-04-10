namespace Web.ViewModels.Seedings
{
    public class SeedingSearchViewModel
    {
        public string WorkingSeason { get; init; } = default!;

        public IEnumerable<string> WorkingSeasons { get; set; } = default!;

        public IEnumerable<SeedingsListingViewModel> Seedings { get; init; } = default!;
    }
}
