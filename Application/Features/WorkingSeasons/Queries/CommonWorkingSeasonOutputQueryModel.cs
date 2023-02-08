namespace Application.Features.WorkingSeasons.Queries
{
    public class CommonWorkingSeasonOutputQueryModel
    {
        public int Id { get; init; }

        public string Name { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
