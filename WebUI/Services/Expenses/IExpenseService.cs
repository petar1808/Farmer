using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Expenses;

namespace WebUI.Services.Expenses
{
    public interface IExpenseService
    {
        Task<List<ListExpensesModel>> List(int seasonId);

        Task<List<SelectionListModel>> GetExpenseTypes();

        Task<List<ExpensesConfigurations>> GetExpensesConfigurations();

        Task<bool> Add(DetailsExpenseModel model);
    }
}
