using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Pages.Seedings.Dialogs;
using WebUI.Services.Seeding;
using WebUI.Services.Treatment;
using WebUI.ServicesModel.Тreatment;
using WebUI.Store;

namespace WebUI.Pages.Seedings
{
    public partial class ListTreatment
    {
        [Inject]
        public ITreatmentService TreatmentService { get; set; } = default!;

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

        private DynamicDataGridModel<GetTreatmentModel> DataGrid { get; set; } = default!;


        protected async override Task OnParametersSetAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TreatmentType), "Тип"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleName), "Артикул", width:"170px"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleQuantity), "Кол. на декар", "{0:0.00} кг/л.", width:"130px"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePrice), "Цена за кг/л.", "{0:0.00} лв." , width:"130px"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.AmountOfFuel), "Изразходвано гориво", "{0:0.00} л." , width:"170px"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPrice), "Цена на гориво", "{0:0.00} лв.", width:"130px"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePriceTotal), "Разход за артикул", "{0:0.00} лв." , width:"150px"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPriceTotal), "Разход за гориво", "{0:0.00} лв.", width:"150px"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TotalCost), "Общ разход", "{0:0.00} лв."),
            };
            DataGrid = new DynamicDataGridModel<GetTreatmentModel>(await TreatmentService.List(SeedingId), columns, "Третиране")
                .WithAdd(async () => await AddTreatment())
                .WithEdit(async (x) => await EditTreatment(x))
                .WithDelete(async (x) => await DeleteTreatment(x))
                .WithPaging()
                .WithSorting()
                .WithResizable()
                .WithDefaultWidth("100px");
        }

        public async Task AddTreatment()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsTreatmentDialog>(
                $"Добавяне на Третиране за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } }, 
                options: DialogOptionsHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await TreatmentService.List(SeedingId));
                await UpdateArableLandBalance(this.SeedingId);
                this.StateHasChanged();
            }
        }

        public async Task EditTreatment(int treatmentId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsTreatmentDialog>($"Редактиране на Третиране за земя: {ArableLandName}-{SizeInDecar} декара",
              new Dictionary<string, object>() { { "TreatmentId", treatmentId } },
              options: DialogOptionsHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await TreatmentService.List(SeedingId));
                await UpdateArableLandBalance(this.SeedingId);
                this.StateHasChanged();
            }
        }

        public async Task DeleteTreatment(int treatmentId)
        {
            if (await DialogService.ShowDeleteDialog(treatmentId) == true)
            {
                if (await this.TreatmentService.Delete(treatmentId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != treatmentId));
                    await UpdateArableLandBalance(this.SeedingId);
                    this.StateHasChanged();
                }
            }
        }

        public async Task<bool> DeleteTreatmentFunction(int performedWorkId)
        {
            return await this.TreatmentService.Delete(performedWorkId);
        }

        private async Task UpdateArableLandBalance(int seedingId)
        {
            this.Dispatcher.Dispatch(
                new UpdateSeedingArableLandBalance(await SeedingService.GetArableLandBalance(seedingId))
                );
        }
    }
}
