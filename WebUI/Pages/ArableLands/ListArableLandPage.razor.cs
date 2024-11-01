using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Extensions;
using WebUI.Services.ArableLand;
using WebUI.ServicesModel.ArableLand;

namespace WebUI.Pages.ArableLands
{
    public partial class ListArableLandPage
    {
        [Inject]
        public IArableLandService ArableLandService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public DynamicDataGridModel<ArableLandModel> DataGrid { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ArableLandModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(ArableLandModel.Name), "Име", filterable : true),
                new DynamicDataGridColumnModel(nameof(ArableLandModel.SizeInDecar), "Декари"),
            };
            DataGrid = new DynamicDataGridModel<ArableLandModel>(
                    await ArableLandService.List(),
                    columns,
                    "Земи")
                .WithAdd(async () => await AddArableLand())
                .WithEdit(async (x) => await EditArableLand(x))
                .WithDelete(async (x) => await DeleteArableLand(x))
                .WithFiltering()
                .WithResizable()
                .WithPaging()
                .WithSorting();
        }

        public async Task EditArableLand(int arableLandId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsArableLand>(
                $"Редактиране на Земя",
                new Dictionary<string, object>() { { "ArableLandId", arableLandId } },
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await ArableLandService.List());
                this.StateHasChanged();
            }
        }

        public async Task AddArableLand()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsArableLand>(
                $"Добавяне на Земя",
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await ArableLandService.List());
                this.StateHasChanged();
            }
        }

        public async Task DeleteArableLand(int arableLandId)
        {
            if (await DialogService.ShowDeleteDialog(arableLandId) == true)
            {
                if (await this.ArableLandService.Delete(arableLandId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(c => c.Id != arableLandId));
                    this.StateHasChanged();
                }
            }
        }
    }
}
