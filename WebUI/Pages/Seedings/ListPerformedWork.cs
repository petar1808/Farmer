using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Pages.Seedings.Dialogs;
using WebUI.Services.PerformedWork;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.PerformedWork;
using WebUI.Store;

namespace WebUI.Pages.Seedings
{
    public partial class ListPerformedWork
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
        public IDispatcher Dispatcher { get; set; } = default!;

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        public DynamicDataGridModel<ListPerformedWorkModel> DataGrid { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ListPerformedWorkModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(ListPerformedWorkModel.WorkType), "Тип обработка"),
                new DynamicDataGridColumnModel(nameof(ListPerformedWorkModel.FuelPrice), "Цена на литър", "{0:0.00} лв."),
                new DynamicDataGridColumnModel(nameof(ListPerformedWorkModel.AmountOfFuel), "Количество гориво общо", "{0:0.00} л."),
                new DynamicDataGridColumnModel(nameof(ListPerformedWorkModel.FuelPriceTotal), "Разход за гориво", "{0:0.00} лв."),
            };
            DataGrid = new DynamicDataGridModel<ListPerformedWorkModel>(await PerformedWorkService.List(SeedingId), columns, "Обработки")
                .WithAdd(async () => await AddPerformedWork())
                .WithEdit(async (x) => await EditPerformedWork(x))
                .WithDelete(async (x) => await DeletePerformedWork(x))
                .WithPaging()
                .WithSorting();
        }

        public async Task AddPerformedWork()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsPerformedWorkDialog>(
                $"Добавяне на Обработка за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogOptionsHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
                await UpdateArableLandBalance(this.SeedingId);
                this.StateHasChanged();
            }
        }
        public async Task EditPerformedWork(int performedWorkId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Редактиране на Обработка за земя: {ArableLandName}-{SizeInDecar} декара",
              new Dictionary<string, object>() { { "PerformedWorkId", performedWorkId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("435px").WithWidth("600px"));

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
                await UpdateArableLandBalance(this.SeedingId);
                this.StateHasChanged();
            }
        }

        public async Task DeletePerformedWork(int performedWorkId)
        {
            if (await DialogService.ShowDeleteDialog(performedWorkId) == true)
            {
                if (await this.PerformedWorkService.Delete(performedWorkId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != performedWorkId));
                    await UpdateArableLandBalance(this.SeedingId);
                    this.StateHasChanged();
                }
            }
        }

        private async Task UpdateArableLandBalance(int seedingId)
        {
            this.Dispatcher.Dispatch(
                new UpdateSeedingArableLandBalance(await SeedingService.GetArableLandBalance(seedingId))
                );
        }
    }
}
