using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Extensions;
using WebUI.Pages.Subsidies;
using WebUI.Services.Expenses;
using WebUI.Services.Subsidies;
using WebUI.ServicesModel.Expenses;
using WebUI.ServicesModel.Subsidies;
using WebUI.Store.WorkingSeason;

namespace WebUI.Pages.Expenses
{
    public partial class ListExpensesPage
    {
        [Inject]
        public IExpenseService ExpenseService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public DynamicDataGridModel<ListExpensesModel> DataGrid { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedWorkingSeasonState { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ListExpensesModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(ListExpensesModel.Type), "Тип разход"),
                new DynamicDataGridColumnModel(nameof(ListExpensesModel.Sum), "Сума", "{0:n2} лв.", total: GetTotal),
                new DynamicDataGridColumnModel(nameof(ListExpensesModel.Article), "Артикул")
            };
            DataGrid = new DynamicDataGridModel<ListExpensesModel>(
                    await ExpenseService.List(SelectedWorkingSeasonState.Value.WorkingSeasonId),
                    columns,
                    "Разходи")
                .WithAdd(async () => await AddExpense())
                .WithEdit(async (x) => await EditExpense(x))
                .WithDelete(async (x) => await DeleteExpense(x))
                .WithPaging()
                .WithSorting();
        }

        public string GetTotal()
        {
            return $"{DataGrid.Data?.Sum(x => x.Sum).ToString("N2") ?? ""} лв.";
        }

        public async Task UpdateDataGrid()
        {
            DataGrid.UpdateData(await ExpenseService.List(SelectedWorkingSeasonState.Value.WorkingSeasonId));
        }

        public async Task AddExpense()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsExpenseDialog>(
                    $"Добавяне на разход за сезон {SelectedWorkingSeasonState.Value.Name}",
                    new Dictionary<string, object>() { { "WorkingSeasonId", SelectedWorkingSeasonState.Value.WorkingSeasonId } },
                    options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await UpdateDataGrid();
                this.StateHasChanged();
            }
        }

        public async Task EditExpense(int expenseId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsExpenseDialog>(
                $"Редактиране на разход за сезон {SelectedWorkingSeasonState.Value.Name}",
                new Dictionary<string, object>() { 
                    { "WorkingSeasonId", SelectedWorkingSeasonState.Value.WorkingSeasonId },
                    { "ExpenseId" , expenseId }
                },
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await UpdateDataGrid();
                this.StateHasChanged();
            }
        }

        public async Task DeleteExpense(int expenseId)
        {
            if (await DialogService.ShowDeleteDialog(expenseId) == true)
            {
                if (await this.ExpenseService.Delete(expenseId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(c => c.Id != expenseId));
                    this.StateHasChanged();
                }
            }
        }
    }
}
