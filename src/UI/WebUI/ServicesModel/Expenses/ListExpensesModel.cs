using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Expenses
{
    public class ListExpensesModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; } = string.Empty;

        public string Article { get; set; } = string.Empty;

        public decimal Sum { get; set; }

        public Dictionary<string, decimal> ExpensesByArableLand { get; set; } = new Dictionary<string, decimal>();
    }
}
