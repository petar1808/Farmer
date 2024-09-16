using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Subsidies
{
    public class SubsidiesModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public decimal? Income { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public ICollection<SubsidySlitByArableLand> ArableLands { get; set; } = new List<SubsidySlitByArableLand>();
    }

    public class SubsidySlitByArableLand
    {
        public string ArableLandName { get; set; } = string.Empty;

        public decimal Income { get; set; }
    }
}
