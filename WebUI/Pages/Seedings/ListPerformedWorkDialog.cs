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

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        public DynamicDataGridModel<GetPerformedWorkModel> DataGrid { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
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

        public async Task<bool> DeletePerformedWorkFunction(int performedWorkId)
        {
           return await this.PerformedWorkService.Delete(performedWorkId);
        }

        public async Task AddPerformedWork()
        {
            await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Добавяне на Обработка",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                new DialogOptions() { Width = "600px", Height = "390px" });

            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
            this.StateHasChanged();
        }
        public async Task EditPerformedWork(int performedWorkId)
        {
            await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Редактиране на Обработка",
              new Dictionary<string, object>() { { "PerformedWorkId", performedWorkId } },
              new DialogOptions() { Width = "600px", Height = "390px" });

            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
            this.StateHasChanged();
        }

        public async Task DeletePerformedWork(int performedWorkId)
        {
            Func<int, Task<bool>> deleteFunction = (id) =>
            {
                var funcResult = DeletePerformedWorkFunction(performedWorkId);
                return funcResult;
            };


            var deleteModel = new DeleteModalModel(performedWorkId, deleteFunction);
            var dialogResult = await DialogService.OpenAsync<DeleteModal>($"Работа",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: new DialogOptions() { Width = "500px", Height = "160px" });

            if (dialogResult == true)
            {
                DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != performedWorkId));

                this.StateHasChanged();
            }
        }
    }
}
