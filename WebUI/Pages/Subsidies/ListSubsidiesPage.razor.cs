using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Services.Subsidies;
using WebUI.ServicesModel.Subsidies;
using WebUI.Extensions;
using Fluxor;
using WebUI.Store.WorkingSeason;

namespace WebUI.Pages.Subsidies
{
    public partial class ListSubsidiesPage
    {
        [Inject]
        public ISubsidyService SubsidyService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public DynamicDataGridModel<ListSubsidiesModel> DataGrid { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedWorkingSeasonState { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ListSubsidiesModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(ListSubsidiesModel.Income), "Приход", "{0:n2} лв.", total: GetTotal),
                new DynamicDataGridColumnModel(nameof(ListSubsidiesModel.Comment), "Бележка")
            };
            DataGrid = new DynamicDataGridModel<ListSubsidiesModel>(
                    await SubsidyService.List(SelectedWorkingSeasonState.Value.WorkingSeasonId),
                    columns,
                    "Земи")
                .WithAdd(async () => await AddSubsidy())
                .WithEdit(async (x) => await EditSubsidy(x))
                .WithDelete(async (x) => await DeleteSubsidy(x))
                .WithPaging()
                .WithSorting();
        }

        public decimal GetTotal()
        {
            return DataGrid.Data.Sum(x => x.Income) ?? 0;
        }

        public async Task UpdateDataGrid()
        {
            DataGrid.UpdateData(await SubsidyService.List(SelectedWorkingSeasonState.Value.WorkingSeasonId));
        }

        public async Task AddSubsidy()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsSubsidyDialog>(
                    $"Добавяне на субсидия за сезон {SelectedWorkingSeasonState.Value.Name}",
                    new Dictionary<string, object>() { { "WorkingSeasonId", SelectedWorkingSeasonState.Value.WorkingSeasonId } },
                    options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await UpdateDataGrid();
                this.StateHasChanged();
            }
        }

        public async Task EditSubsidy(int subsidyId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsSubsidyDialog>(
                    $"Редактиране на субсидия за сезон: {SelectedWorkingSeasonState.Value.Name}",
                    new Dictionary<string, object>() { 
                            { "SubsidyId", subsidyId },
                            { "WorkingSeasonId", SelectedWorkingSeasonState.Value.WorkingSeasonId }
                    },
                    options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await UpdateDataGrid();
                this.StateHasChanged();
            }
        }

        public async Task DeleteSubsidy(int subsidyId)
        {
            if (await DialogService.ShowDeleteDialog(subsidyId) == true)
            {
                if (await this.SubsidyService.Delete(subsidyId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != subsidyId));
                    this.StateHasChanged();
                }
            }
        }
    }
}
