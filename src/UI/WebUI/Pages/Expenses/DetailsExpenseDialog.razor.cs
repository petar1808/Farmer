using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Article;
using WebUI.Services.Expenses;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;
using WebUI.ServicesModel.Expenses;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Expenses
{
    public partial class DetailsExpenseDialog
    {
        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public IExpenseService ExpenseService { get; set; } = default!;

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Parameter]
        public int WorkingSeasonId { get; set; }

        [Parameter]
        public int ExpenseId { get; set; }

        public List<ExpensesConfigurations> ExpensesConfigurations { get; set; }

        public ExpensesConfigurations ExpenseConfiguration { get; set; }

        public List<SownArableLandModel> SownArableLands { get; set; } = new List<SownArableLandModel>();

        public DetailsExpenseModel Expense { get; set; } = default!;

        public List<SelectionListModel> ExpenseTypes { get; set; }

        public List<SelectionListModel> Articles { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {
            SownArableLands = await SeedingService.GetSownArableLands(WorkingSeasonId);
            ExpensesConfigurations = await ExpenseService.GetExpensesConfigurations();
            ExpenseTypes = await ExpenseService.GetExpenseTypes();

            if (ExpenseId == 0)
            {
                ExpenseConfiguration = new ExpensesConfigurations();
                Expense = new DetailsExpenseModel();
                Expense.WorkingSeasonId = WorkingSeasonId;
            }
            else
            {
                Expense = await ExpenseService.Get(ExpenseId);
                ExpenseConfiguration = ExpensesConfigurations.First(x => (int)x.ExpenseType == Expense.Type);
                if (ExpenseConfiguration.ArticleType != null)
                {
                    Articles = await ArticleService.GetArticles((ArticleType)ExpenseConfiguration.ArticleType);
                }
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        public async Task OnExpenseTypeChange(object value)
        {
            ExpenseConfiguration = ExpensesConfigurations.First(x => (int)x.ExpenseType == (int)value);
            if (ExpenseConfiguration.ArticleType != null)
            {
                Articles = await ArticleService.GetArticles(ExpenseConfiguration.ArticleType!.Value);
            }

            Expense.ArticleId = null;

            if (ExpenseConfiguration.ShowPricePerUnit == false)
            {
                Expense.Quantity = 1;
                Expense.PricePerUnit = Expense.Sum;
            }

            if (!ExpenseConfiguration.DistributeByArableLand)
            {
                Expense.SelectedArableLands = Enumerable.Empty<int>();
            }
        }

        public void OnPricePerUnitChange(decimal pricePerUnit)
        {
            Expense.Sum = pricePerUnit * Expense.Quantity;
        }

        public void OnQuantityChange(decimal quantity)
        {
            Expense.Sum = quantity * Expense.PricePerUnit;
        }

        public void OnSumChange(decimal sum)
        {
            Expense.Quantity = 1;
            Expense.PricePerUnit = sum;
        }

        protected async Task OnSubmit(DetailsExpenseModel expense)
        {
            bool isSuccess = false;

            if (!ExpenseConfiguration.DistributeByArableLand)
            {
                expense.SelectedArableLands = new List<int>();
            }
            if (ExpenseConfiguration.ArticleType is null)
            {
                expense.ArticleId = null;
            }

            if (ExpenseId == 0)
            {
                isSuccess = await ExpenseService.Add(expense);
            }
            else
            {
                isSuccess = await ExpenseService.Update(expense);
            }
            DialogService.Close(isSuccess);
        }
    }
}
