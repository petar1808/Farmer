using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Services.PerformedWork;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Pages.Seedings
{
    public partial class ListPerformedWorkDialog
    {
        [Inject]
        public IPerformedWorkService PerformedWorkService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public DynamicDataGridModel<GetPerformedWorkModel> DataGrid { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.Date), "Дата"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.WorkType), "Тип"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.AmountOfFuel), "Гориво"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.FuelPrice), "Цена"),
            };
            DataGrid = new DynamicDataGridModel<GetPerformedWorkModel>(
                    await PerformedWorkService.List(SeedingId),
                    columns)
                .WithEdit(async (x) => await EditPerformedWork(x))
                .WithDelete(async (x) => await DeletePerformedWork(x))
                .WithFiltering()
                .WithPaging()
                .WithSorting();
        }

        public async Task DeletePerformedWorkAction(int performedWorkId)
        {
            await this.PerformedWorkService.Delete(performedWorkId);
        }

        public async Task AddPerformedWork()
        {
            await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Работа",
              options: new DialogOptions() { Width = "700px", Height = "570px" });

            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
            this.StateHasChanged();
        }
        public async Task EditPerformedWork(int performedWorkId)
        {
            await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Работа {performedWorkId}",
              new Dictionary<string, object>() { { "PerformedWorkId", performedWorkId } },
              new DialogOptions() { Width = "700px", Height = "570px" });

            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
            this.StateHasChanged();
        }


        public async Task DeletePerformedWork(int performedWorkId)
        {
            var deleteModel = new DeleteModalModel(performedWorkId, async (id) => await DeletePerformedWorkAction(id));
            await DialogService.OpenAsync<DeleteModal>($"Работа",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: new DialogOptions() { Width = "500px", Height = "160px" });
            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
            this.StateHasChanged();
        }
    }
}
