using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Pages.WorkingSeasons
{
    public partial class WorkingSeasonsPage
    {
        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public NavigationManager UriHelper { get; set; } = default!;

        public List<WorkingSeasonModel> WorkingSeasons { get; set; } = new List<WorkingSeasonModel>();

        public List<ListWorkingSeasonBalanceModel> WorkingSeasonBalance { get; set; } = new List<ListWorkingSeasonBalanceModel>();

        protected override async Task OnInitializedAsync()
        {
            WorkingSeasonBalance = await WorkingSeasonService.ListWorkingSeasonsBalance();
        }

        public async Task AddWorkingSeason()
        {
            var dialogResult = await DialogService.OpenAsync<WorkingSeasonDialog>($"Добавяне на Сезон",
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("330px").WithWidth("600px"));

            if (dialogResult == true)
            {
                WorkingSeasonBalance = await WorkingSeasonService.ListWorkingSeasonsBalance();
                this.StateHasChanged();
            }
        }
        public async Task EditWorkingSeason(int workingSeasonId)
        {
            var dialogResult = await DialogService.OpenAsync<WorkingSeasonDialog>($"Редактиране на Сезон",
              new Dictionary<string, object>() { { "WorkingSeasonId", workingSeasonId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("330px").WithWidth("600px"));

            if (dialogResult == true)
            {
                WorkingSeasonBalance = await WorkingSeasonService.ListWorkingSeasonsBalance();
                this.StateHasChanged();
            }
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
                WorkingSeasonBalance = await WorkingSeasonService.ListWorkingSeasonsBalance();

                this.StateHasChanged();
            }
        }

        public void ToSeeding(int workingSeasonId)
        {
            UriHelper.NavigateTo($"{UriHelper.Uri}/{workingSeasonId}/seeding");
        }

        private async Task<bool> DeleteWorkingSeasonFunction(int workingSeasonId)
        {
            return await this.WorkingSeasonService.Delete(workingSeasonId);
        }
    }
}
