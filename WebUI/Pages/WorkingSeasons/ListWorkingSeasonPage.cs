using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
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
                new DynamicDataGridColumnModel(nameof(WorkingSeasonModel.StartDate), "Начало"),
                new DynamicDataGridColumnModel(nameof(WorkingSeasonModel.EndDate), "Край"),
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

        public async Task DeleteWorkingSeasonAction(int workingSeasonId)
        {
            await this.WorkingSeasonService.Delete(workingSeasonId);
        }

        public async Task AddWorkingSeason()
        {
            await DialogService.OpenAsync<DetailsWorkingSeason>($"Сезон",
              options: new DialogOptions() { Width = "700px", Height = "570px" });

            DataGrid.UpdateData(await WorkingSeasonService.List());
            this.StateHasChanged();
        }
        public async Task EditWorkingSeason(int workingSeasonId)
        {
            await DialogService.OpenAsync<DetailsWorkingSeason>($"Сезон",
              new Dictionary<string, object>() { { "WorkingSeasonId", workingSeasonId } },
              new DialogOptions() { Width = "700px", Height = "570px" });

            DataGrid.UpdateData(await WorkingSeasonService.List());
            this.StateHasChanged();
        }

        public async Task DeleteWorkingSeason(int workingSeasonId)
        {
            var deleteModel = new DeleteModalModel(workingSeasonId, async (id) => await DeleteWorkingSeasonAction(id));
            await DialogService.OpenAsync<DeleteModal>($"Сезон",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: new DialogOptions() { Width = "500px", Height = "160px" });
            DataGrid.UpdateData(await WorkingSeasonService.List());
            this.StateHasChanged();
        }
    }
}
