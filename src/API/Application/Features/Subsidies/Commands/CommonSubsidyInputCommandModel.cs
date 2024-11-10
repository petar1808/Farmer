namespace Application.Features.Subsidies.Commands
{
    public class CommonSubsidyInputCommandModel
    {
        public decimal Income { get; set; }

        public DateTime Date { get; set; }

        public string Comment { get; set; } = string.Empty;

        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();
    }
}
