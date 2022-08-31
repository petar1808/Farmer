using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.PerformedWork
{
    public class GetPerformedWorkModel : PerformedWorkBaseModel, IDynamicDataGridModel
    {
        public string WorkType { get; set; } = default!;
    }
}
