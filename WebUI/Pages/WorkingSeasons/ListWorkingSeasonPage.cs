using Microsoft.AspNetCore.Components;
using Radzen;
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

        public List<GetWorkingSeasonApiModel> WorkingSeasons { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            WorkingSeasons = await WorkingSeasonService.List();
        }


        public async Task EditWorkingSeason(int workingSeasonId)
        {
            await DialogService.OpenAsync<DetailsWorkingSeasonPage>($"Сезон {workingSeasonId}",
              new Dictionary<string, object>() { { "WorkingSeasonId", workingSeasonId } },
              new DialogOptions() { Width = "700px", Height = "570px" });

            WorkingSeasons = await WorkingSeasonService.List();
        }

        public async Task AddWorkingSeason()
        {
            await DialogService.OpenAsync<DetailsWorkingSeasonPage>($"Сезон",
              options: new DialogOptions() { Width = "700px", Height = "570px" });

            WorkingSeasons = await WorkingSeasonService.List();
        }

        public async Task DeleteWorkingSeason(int articleId)
        {
            await DialogService.OpenAsync<DetailsWorkingSeasonPage>($"Сезон",
              options: new DialogOptions() { Width = "700px", Height = "570px" });
            WorkingSeasons = await WorkingSeasonService.List();
        }
    }
}
