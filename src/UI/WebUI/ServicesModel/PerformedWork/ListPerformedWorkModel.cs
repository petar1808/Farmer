using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.PerformedWork
{
    public class ListPerformedWorkModel : PerformedWorkBaseModel, IDynamicDataGridModel
    {
        public string WorkType { get; set; } = default!;
    }
}
