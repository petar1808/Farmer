﻿using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
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
                new DynamicDataGridColumnModel(nameof(ArableLandModel.Name), "Име"),
                new DynamicDataGridColumnModel(nameof(ArableLandModel.SizeInDecar), "Декари"),
            };
            DataGrid = new DynamicDataGridModel<ArableLandModel>(
                    await ArableLandService.List(),
                    columns)
                .WithEdit(async (x) => await EditArableLand(x))
                .WithDelete(async (x) => await DeleteArableLand(x))
                .WithFiltering()
                .WithPaging()
                .WithSorting();
        }

        public async Task<bool> DeleteArableLandFunction(int articleId)
        {
            return await this.ArableLandService.Delete(articleId);
        }

        public async Task EditArableLand(int arableLandId)
        {
            await DialogService.OpenAsync<DetailsArableLand>($"Редактиране на Земя",
              new Dictionary<string, object>() { { "ArableLandId", arableLandId } },
              new DialogOptions() { Width = "600px", Height = "275px" });

            DataGrid.UpdateData(await ArableLandService.List());
            this.StateHasChanged();
        }

        public async Task AddArableLand()
        {
            await DialogService.OpenAsync<DetailsArableLand>($"Добавяне на Земя",
              options: new DialogOptions() { Width = "600px", Height = "275px" });

            DataGrid.UpdateData(await ArableLandService.List());
        }

        public async Task DeleteArableLand(int arableLandId)
        {
            Func<int, Task<bool>> deleteFunction = (id) => 
            {
                var funcResult = DeleteArableLandFunction(id);
                return funcResult;
            };

            var deleteModel = new DeleteModalModel(arableLandId, deleteFunction);
            var dialogResult = await DialogService.OpenAsync<DeleteModal>($"Изтриване на Земя",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: new DialogOptions() { Width = "500px", Height = "160px" });

            if (dialogResult == true)
            {
                DataGrid.UpdateData(DataGrid.Data.Where(c => c.Id != arableLandId));

                this.StateHasChanged();
            }
        }
    }
}
