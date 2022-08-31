using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Тreatment
{
    public class GetTreatmentModel : ТreatmentBaseModel, IDynamicDataGridModel
    {
        public string ТreatmentType { get; set; } = default!;
    }
}
