using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Services.Treatment;
using WebUI.ServicesModel.Тreatment;

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
        public EventCallback<int> OnChangeData { get; set; }

        private DynamicDataGridModel<GetTreatmentModel> DataGrid { get; set; } = default!;


        protected async override Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TreatmentType), "Тип третиране"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleName), "Препарат/Тор"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleQuantity), "Количество препарат/тор на декар кг/л"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePrice), "Цена на препарат/тор за л/кг"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.AmountOfFuel), "Количество гориво"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPrice), "Цена на гориво на л"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.FuelPriceTotal), "Разход за гориво"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticlePriceTotal), "Разход препарат/тор"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TotalCost), "Общ разход"),
            };
            DataGrid = new DynamicDataGridModel<GetTreatmentModel>(await TreatmentService.List(SeedingId),columns)
                .WithEdit(async (x) => await EditTreatment(x))
                .WithDelete(async (x) => await DeleteTreatment(x))
                .WithFiltering()
                .WithPaging()
                .WithSorting();
        }

        public async Task AddTreatment()
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Добавяне на Третиране за земя: {ArableLandName}",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("600px").WithWidth("600px"));


            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            await this.OnChangeData.InvokeAsync(this.SeedingId);
            this.StateHasChanged();
        }

        public async Task EditTreatment(int treatmentId)
        {
            await DialogService.OpenAsync<DetailsTreatmentDialog>($"Редактиране на Третиране за земя: {ArableLandName}",
              new Dictionary<string, object>() { { "TreatmentId", treatmentId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("600px").WithWidth("600px"));

            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            await this.OnChangeData.InvokeAsync(this.SeedingId);
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
                await this.OnChangeData.InvokeAsync(this.SeedingId);
                this.StateHasChanged();
            }
        }

        public async Task<bool> DeleteTreatmentFunction(int performedWorkId)
        {
            return await this.TreatmentService.Delete(performedWorkId);
        }
    }
}
