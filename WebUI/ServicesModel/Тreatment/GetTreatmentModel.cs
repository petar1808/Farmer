using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Тreatment
{
    public class GetTreatmentModel : ТreatmentBaseModel, IDynamicDataGridModel
    {
        public string ArticleName { get; set; } = default!;

        public string TreatmentType { get; set; } = default!;

        public decimal? FuelPriceTotal => FuelPrice * AmountOfFuel;

        public decimal ArticlePriceTotal { get; set; }

        public decimal? TotalCost => FuelPriceTotal + ArticlePriceTotal;
    }
}
