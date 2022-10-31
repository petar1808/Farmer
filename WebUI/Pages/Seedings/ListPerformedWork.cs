using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Services.PerformedWork;
using WebUI.ServicesModel.PerformedWork;

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

        [Parameter]
        public EventCallback<int> OnChangeData { get; set; }

        public DynamicDataGridModel<GetPerformedWorkModel> DataGrid { get; set; } = default!;


        protected async override Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.WorkType), "Тип обработка"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.FuelPrice), "Цена на литър(лв.)"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.AmountOfFuel), "Количество гориво(л.)"),
                new DynamicDataGridColumnModel(nameof(GetPerformedWorkModel.FuelPriceTotal), "Разход за гориво(лв.)"),
            };
            DataGrid = new DynamicDataGridModel<GetPerformedWorkModel>(await PerformedWorkService.List(SeedingId), columns)
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
            await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Добавяне на Обработка за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("435px").WithWidth("600px"));

            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));

            await this.OnChangeData.InvokeAsync(this.SeedingId);
            this.StateHasChanged();
        }
        public async Task EditPerformedWork(int performedWorkId)
        {
            await DialogService.OpenAsync<DetailsPerformedWorkDialog>($"Редактиране на Обработка за земя: {ArableLandName}-{SizeInDecar} декара",
              new Dictionary<string, object>() { { "PerformedWorkId", performedWorkId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("435px").WithWidth("600px"));

            DataGrid.UpdateData(await PerformedWorkService.List(SeedingId));
            await this.OnChangeData.InvokeAsync(this.SeedingId);
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
              options: DialogOptionsHelper.GetDeleteDialogDefaultOptions().WithDefaultSize());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != performedWorkId));
                await this.OnChangeData.InvokeAsync(this.SeedingId);
                this.StateHasChanged();
            }
        }
    }
}
