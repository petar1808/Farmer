using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Extensions;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Pages.FarmingSeason
{
    public partial class ListFarmingSeasonPage
    {
        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public DynamicDataGridModel<WorkingSeasonModel> DataGrid { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new(nameof(WorkingSeasonModel.Id), "Ид"),
                new(nameof(WorkingSeasonModel.Name), "Име"),
                new(nameof(WorkingSeasonModel.StartDate), "Начална Дата", "{0:dd/MM/yy}"),
                new(nameof(WorkingSeasonModel.EndDate), "Крайна Дата", "{0:dd/MM/yy}")
            };

            DataGrid = new DynamicDataGridModel<WorkingSeasonModel>(
                    await WorkingSeasonService.List(),
                    columns,
                    "Сезони")
                .WithAdd(async () => await AddWorkingSeason())
                .WithEdit(async (x) => await EditWorkingSeason(x))
                .WithDelete(async (x) => await DeleteWorkingSeason(x))
                .WithPaging()
                .WithSorting();

        }

        public async Task AddWorkingSeason()
        {
            var dialogResult = await DialogService.OpenAsync<FarmingSeasonDialog>(
                $"Добавяне на Сезон",
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await WorkingSeasonService.List());
                this.StateHasChanged();
            }
        }

        public async Task EditWorkingSeason(int workingSeasonId)
        {
            var dialogResult = await DialogService.OpenAsync<FarmingSeasonDialog>(
                $"Редактиране на Сезон",
                new Dictionary<string, object>() { { "WorkingSeasonId", workingSeasonId } },
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await WorkingSeasonService.List());
                this.StateHasChanged();
            }
        }

        public async Task DeleteWorkingSeason(int workingSeasonId)
        {
            if (await DialogService.ShowDeleteDialog(workingSeasonId) == true)
            {
                if (await this.WorkingSeasonService.Delete(workingSeasonId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(c => c.Id != workingSeasonId));
                    this.StateHasChanged();
                }
            }
        }
    }
}
