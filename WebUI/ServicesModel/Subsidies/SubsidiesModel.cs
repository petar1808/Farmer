using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Subsidies
{
    public class SubsidiesModel : SubsidiesBaseModel, IDynamicDataGridModel
    {
        public int SeasonId { get; set; }

        public Dictionary<string, decimal> IncomeByArableLand { get; set; } = new Dictionary<string, decimal>();

        public IEnumerable<int> ArableLandIds { get; set; } = Enumerable.Empty<int>();
    }

    public class SubsidySlitByArableLand
    {
        public string ArableLandName { get; set; } = string.Empty;

        public decimal Income { get; set; }
    }
}
