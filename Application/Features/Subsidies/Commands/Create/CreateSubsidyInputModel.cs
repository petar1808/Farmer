namespace Application.Features.Subsidies.Commands.Create
{
    public class CreateSubsidyInputModel : CommonSubsidyInputCommandModel
    {
        public int SeasonId { get; set; }

        public IEnumerable<int> ArableLandIds { get; set; } = Enumerable.Empty<int>();
    }
}
