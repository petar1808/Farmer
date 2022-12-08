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
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TreatmentType), "Тип третиране"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleName), "Препарат/Тор"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleQuantity), "Количество препарат/тор на декар кг/л"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePrice), "Цена на препарат/тор за л/кг"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.AmountOfFuel), "Количество гориво общо(литър)"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPrice), "Цена на гориво на литър"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPriceTotal), "Разход за гориво"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePriceTotal), "Разход препарат/тор"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TotalCost), "Общ разход"),
            };
            DataGrid = new DynamicDataGridModel<GetTreatmentModel>(await TreatmentService.List(SeedingId),columns)
                .WithEdit(async (x) => await EditTreatment(x))
                .WithDelete(async (x) => await DeleteTreatment(x))
                .WithPaging()
                .WithSorting()
                .WithResizable();
        }

        public async Task AddTreatment()
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Добавяне на Третиране за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("600px").WithWidth("600px"));

            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            await UpdateArableLandBalance(this.SeedingId);
            this.StateHasChanged();
        }

        public async Task EditTreatment(int treatmentId)
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Редактиране на Третиране за земя: {ArableLandName}-{SizeInDecar} декара",
              new Dictionary<string, object>() { { "TreatmentId", treatmentId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("600px").WithWidth("600px"));

            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            await UpdateArableLandBalance(this.SeedingId);
            this.StateHasChanged();
        }

        public async Task DeleteTreatment(int treatmentId)
        {
            Func<int, Task<bool>> deleteFunction = (id) =>
            {
                var funcResult = DeleteTreatmentFunction(treatmentId);
                return funcResult;
            };

            var deleteModel = new DeleteModalModel(treatmentId, deleteFunction);
            var dialogResult = await DialogService.OpenAsync<DeleteModal>($"Третиране",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: DialogOptionsHelper.GetDeleteDialogDefaultOptions().WithDefaultSize());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != treatmentId));
                await UpdateArableLandBalance(this.SeedingId);
                this.StateHasChanged();
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
