using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Expenses;

namespace WebUI.Services.Expenses
{
    public interface IExpenseService
    {
        Task<List<ListExpensesModel>> List(int seasonId);

        Task<DetailsExpenseModel> Get(int id);

        Task<List<SelectionListModel>> GetExpenseTypes();

        Task<List<ExpensesConfigurations>> GetExpensesConfigurations();

        Task<bool> Add(DetailsExpenseModel model);

        Task<bool> Update(DetailsExpenseModel article);

        Task<bool> Delete(int id);
    }
}
