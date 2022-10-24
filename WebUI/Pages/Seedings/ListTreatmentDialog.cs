using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Services.Treatment;
using WebUI.ServicesModel.Тreatment;

namespace WebUI.Pages.Seedings
{
    public partial class ListTreatmentDialog
    {
        [Inject]
        public ITreatmentService TreatmentService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public string ArableLandName { get; set; } = default!;

        public DynamicDataGridModel<GetTreatmentModel> DataGrid { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Date), "Дата"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TreatmentType), "Третиране"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleName), "Препарат"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleQuantity), "Количество на декар"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.AmountOfFuel), "Гориво за цялата нива"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPrice), "Цена на гориво на л."),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePrice), "Цена на артикул на декър"),
            };
            DataGrid = new DynamicDataGridModel<GetTreatmentModel>(
                    await TreatmentService.List(SeedingId),
                    columns)
                .WithEdit(async (x) => await EditTreatment(x))
                .WithDelete(async (x) => await DeleteTreatment(x))
                .WithFiltering()
                .WithPaging()
                .WithSorting();
        }

        public async Task<bool> DeleteTreatmentFunction(int performedWorkId)
        {
            return await this.TreatmentService.Delete(performedWorkId);
        }

        public async Task AddTreatment()
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Добавяне на Третиране за земя: {ArableLandName}",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("550px").WithWidth("600px"));

            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            this.StateHasChanged();
        }

        public async Task EditTreatment(int treatmentId)
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Редактиране на Третиране за земя: {ArableLandName}",
              new Dictionary<string, object>() { { "TreatmentId", treatmentId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("550px").WithWidth("600px"));

            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
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

                this.StateHasChanged();
            }
        }
    }
}
