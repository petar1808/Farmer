using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
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

        public DynamicDataGridModel<GetTreatmentModel> DataGrid { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Date), "Дата"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TreatmentType), "Третиране"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleId), "Препарат"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleQuantity), "Препарат на декар"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.AmountOfFuel), "Гориво"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPrice), "Цена на гориво"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePrice), "Цена на препарат"),
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

        public async Task DeleteTreatmentAction(int performedWorkId)
        {
            await this.TreatmentService.Delete(performedWorkId);
        }

        public async Task AddTreatment()
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Добавяне на Третиране",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
              options: new DialogOptions() { Width = "600px", Height = "540px" });

            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            this.StateHasChanged();
        }

        public async Task EditTreatment(int treatmentId)
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Редактиране на Третиране",
              new Dictionary<string, object>() { { "TreatmentId", treatmentId } },
              new DialogOptions() { Width = "600px", Height = "540px" });

            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            this.StateHasChanged();
        }

        public async Task DeleteTreatment(int treatmentId)
        {
            var deleteModel = new DeleteModalModel(treatmentId, async (id) => await DeleteTreatmentAction(id));
            await DialogService.OpenAsync<DeleteModal>($"Третиране",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: new DialogOptions() { Width = "500px", Height = "160px" });
            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            this.StateHasChanged();
        }
    }
}
