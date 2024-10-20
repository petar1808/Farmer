using Fluxor;
using Microsoft.AspNetCore.Components;
using WebUI.Services.Expenses;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Seeding;
using WebUI.Store.WorkingSeason;

namespace WebUI.Pages.Sowing
{
    public partial class ListSowingPage
    {
        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedWorkingSeasonState { get; set; } = default!;

        public List<ListSeedingModel> SeedingData { get; set; } = new List<ListSeedingModel>();


        protected async override Task OnInitializedAsync()
        {
            SeedingData = await SeedingService.ListSeeding(SelectedWorkingSeasonState.Value.WorkingSeasonId);
        }

        public async Task UpdateData()
        {
            SeedingData = await SeedingService.ListSeeding(SelectedWorkingSeasonState.Value.WorkingSeasonId);
        }
    }
}
