using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.WorkingSeason
{
    public class ListWorkingSeasonBalanceModel : WorkingSeasonModel
    {
        public decimal Income { get; set; }

        public decimal Expenses { get; set; }

        public decimal Profit { get; set; }
    }
}
