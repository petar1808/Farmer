using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Extensions;
using WebUI.Pages.Work.Dialogs;
using WebUI.Services.PerformedWork;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.PerformedWork;
using WebUI.Store;
using WebUI.Store.WorkingSeason;

namespace WebUI.Pages.Work
{
    public partial class ListPerformedWorkComponent
    {
        [Inject]
        public IPerformedWorkService PerformedWorkService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public string ArableLandName { get; set; } = default!;

        [Parameter]
        public int SizeInDecar { get; set; }

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        public DynamicDataGridModel<ListPerformedWorkModel> DataGrid { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedWorkingSeasonState { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ListPerformedWorkModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(ListPerformedWorkModel.WorkType), "Тип обработка", filterable : true)
            };
            DataGrid = new DynamicDataGridModel<ListPerformedWorkModel>(await PerformedWorkService.List(SeedingId), columns, "Обработки")
                .WithAdd(async () => await AddPerformedWork())
                .WithEdit(async (x) => await EditPerformedWork(x))
                .WithDelete(async (x) => await DeletePerformedWork(x))
                .WithPaging()
                .WithFiltering()
                .WithSorting()
                .WithResizable();

            SelectedWorkingSeasonState.StateChanged += async (sender, state) => await ReLoadData();
        }

        public async Task AddPerformedWork()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsPerformedWorkDialog>(
                $"Добавяне на Обработка за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await ReLoadData();
            }
        }
        public async Task EditPerformedWork(int performedWorkId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Редактиране на Обработка за земя: {ArableLandName}-{SizeInDecar} декара",
              new Dictionary<string, object>() { { "PerformedWorkId", performedWorkId } },
              options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await ReLoadData();
            }
        }

        public async Task DeletePerformedWork(int performedWorkId)
        {
            if (await DialogService.ShowDeleteDialog(performedWorkId) == true)
            {
                if (await this.PerformedWorkService.Delete(performedWorkId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != performedWorkId));
                    this.StateHasChanged();
                }
            }
        }

        public async Task ReLoadData()
        {
            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
            this.StateHasChanged();
        }
    }
}
