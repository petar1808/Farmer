using Microsoft.AspNetCore.Components;
using Radzen;
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
            var dialogResult = await DialogService.OpenAsync<WorkingSeasonDialog>($"Добавяне на Сезон");

            if (dialogResult == true)
            {
                WorkingSeasonBalance = await WorkingSeasonService.ListWorkingSeasonsBalance();
                this.StateHasChanged();
            }
        }
        public async Task EditWorkingSeason(int workingSeasonId)
        {
            var dialogResult = await DialogService.OpenAsync<WorkingSeasonDialog>($"Редактиране на Сезон",
              new Dictionary<string, object>() { { "WorkingSeasonId", workingSeasonId } });

            if (dialogResult == true)
            {
                WorkingSeasonBalance = await WorkingSeasonService.ListWorkingSeasonsBalance();
                this.StateHasChanged();
            }
        }

        public async Task DeleteWorkingSeason(int workingSeasonId)
        {
            if (await DialogService.ShowDeleteDialog(workingSeasonId) == true)
            {
                if (await this.WorkingSeasonService.Delete(workingSeasonId))
                {
                    WorkingSeasonBalance = await WorkingSeasonService.ListWorkingSeasonsBalance();
                    this.StateHasChanged();
                }
            }
        }

        public void ToSeeding(int workingSeasonId)
        {
            UriHelper.NavigateTo($"{UriHelper.Uri}/{workingSeasonId}/seeding");
        }
    }
}
