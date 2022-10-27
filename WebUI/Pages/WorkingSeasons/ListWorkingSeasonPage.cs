using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Pages.WorkingSeasons
{
    public partial class ListWorkingSeasonPage
    {
        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public DynamicDataGridModel<WorkingSeasonModel> DataGrid { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(WorkingSeasonModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(WorkingSeasonModel.Name), "Име"),
                new DynamicDataGridColumnModel(nameof(WorkingSeasonModel.StartDate), "Начало", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(WorkingSeasonModel.EndDate), "Край", "{0:dd/MM/yy}"),
            };
            DataGrid = new DynamicDataGridModel<WorkingSeasonModel>(
                    await WorkingSeasonService.List(),
                    columns)
                .WithEdit(async (x) => await EditWorkingSeason(x))
                .WithDelete(async (x) => await DeleteWorkingSeason(x))
                .WithFiltering()
                .WithPaging()
                .WithSorting();
        }

        public async Task<bool> DeleteWorkingSeasonFunction(int workingSeasonId)
        {
            return await this.WorkingSeasonService.Delete(workingSeasonId);
        }

        public async Task AddWorkingSeason()
        {
            await DialogService.OpenAsync<DetailsWorkingSeason>($"Добавяне на Сезон",
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("330px").WithWidth("600px"));

            DataGrid.UpdateData(await WorkingSeasonService.List());
            this.StateHasChanged();
        }
        public async Task EditWorkingSeason(int workingSeasonId)
        {
            await DialogService.OpenAsync<DetailsWorkingSeason>($"Редактиране на Сезон",
              new Dictionary<string, object>() { { "WorkingSeasonId", workingSeasonId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("330px").WithWidth("600px"));

            DataGrid.UpdateData(await WorkingSeasonService.List());
            this.StateHasChanged();
        }

        public async Task DeleteWorkingSeason(int workingSeasonId)
        {
            Func<int, Task<bool>> deleteFunction = (id) =>
            {
                var funcResult = DeleteWorkingSeasonFunction(workingSeasonId);
                return funcResult;
            };

            var deleteModel = new DeleteModalModel(workingSeasonId, deleteFunction);
            var dialogResult = await DialogService.OpenAsync<DeleteModal>($"Изтриване на Сезон",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: DialogOptionsHelper.GetDeleteDialogDefaultOptions().WithDefaultSize());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != workingSeasonId));

                this.StateHasChanged();
            }
        }
    }
}
