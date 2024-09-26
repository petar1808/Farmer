using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Subsidies
{
    public class SubsidiesModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public decimal? Income { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public int SeasonId { get; set; }

        public ICollection<SubsidySlitByArableLand> ArableLands { get; set; } = new List<SubsidySlitByArableLand>();

        public IEnumerable<int> ArableLandIds { get; set; } = Enumerable.Empty<int>();
    }

    public class SubsidySlitByArableLand
    {
        public string ArableLandName { get; set; } = string.Empty;

        public decimal Income { get; set; }
    }
}
