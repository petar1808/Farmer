namespace WebUI.ServicesModel.Subsidies
{
    public class DetailsSubsidyModel : SubsidyBaseModel
    {
        public IEnumerable<int> SelectedArableLands { get; set; } = Enumerable.Empty<int>();

        public int SeasonId { get; set; }
    }
}
