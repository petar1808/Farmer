using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Subsidies
{
    public class ListSubsidiesModel : SubsidyBaseModel, IDynamicDataGridModel
    {
        public Dictionary<string, decimal> IncomeByArableLand { get; set; } = new Dictionary<string, decimal>();
    }
}
