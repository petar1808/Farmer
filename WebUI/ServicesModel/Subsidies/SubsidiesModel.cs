using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Subsidies
{
    public class SubsidiesModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public decimal? Income { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
