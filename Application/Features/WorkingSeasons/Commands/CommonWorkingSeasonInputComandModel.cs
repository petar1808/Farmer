namespace Application.Features.WorkingSeasons.Commands
{
    public class CommonWorkingSeasonInputComandModel
    {
        public string Name { get; set; } = default!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
