using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Тreatment
{
    public class GetTreatmentModel : ТreatmentBaseModel, IDynamicDataGridModel
    {
        public string ArticleName { get; set; } = default!;

        public string TreatmentType { get; set; } = default!;

        public decimal SumArticleQuantity { get; set; }
    }
}
