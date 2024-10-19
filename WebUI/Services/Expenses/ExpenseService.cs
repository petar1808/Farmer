using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Expenses;

namespace WebUI.Services.Expenses
{
    public class ExpenseService : IExpenseService
    {
        private readonly IHttpService _httpService;

        public ExpenseService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> Add(DetailsExpenseModel model)
        {
            return await _httpService.PostAsync<bool>("api/expense", model); 
        }

        public async Task<List<ExpensesConfigurations>> GetExpensesConfigurations()
        {
            return await _httpService.GetAsync<List<ExpensesConfigurations>>($"/api/expense/configurations");
        }

        public async Task<List<SelectionListModel>> GetExpenseTypes()
        {
            return await _httpService.GetAsync<List<SelectionListModel>>($"/api/assets/expenseType");
        }

        public async Task<List<ListExpensesModel>> List(int seasonId)
        {
            return await _httpService
                    .GetAsync<List<ListExpensesModel>>($"/api/expense/list/{seasonId}");
        }
    }
}
