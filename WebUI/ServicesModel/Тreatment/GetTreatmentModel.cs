using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Тreatment
{
    public class GetTreatmentModel : ТreatmentBaseModel, IDynamicDataGridModel
    {
        public string TreatmentType { get; set; } = default!;
    }
}
