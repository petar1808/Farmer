using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Тreatment
{
    public class GetTreatmentModel : ТreatmentBaseModel, IDynamicDataGridModel
    {
        public string TypeTreatment { get; set; } = default!;
    }
}
