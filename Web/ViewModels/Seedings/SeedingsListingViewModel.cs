namespace Web.ViewModels.Seedings
{
    public class SeedingsListingViewModel
    {
        public int Id { get; init; }

        public string AreableLandName { get; init; } = default!;

        public string ArticleName { get; init; } = default!;

        public int SizeInDecar { get; init; } = default!;

        public string SeasonName { get; init; } = default!;

        public DateTime? StartDate { get; init; }

        public DateTime? EndDate { get; init; }
    }
}
