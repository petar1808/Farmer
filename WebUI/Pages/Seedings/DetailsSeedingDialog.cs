using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Seedings
{
    public partial class DetailsSeedingDialog
    {
        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public SelectedWorkingSeasonService SelectedWorkingSeasonService { get; set; } = default!;

        public List<SelectionListModel> ArableLands { get; set; } = new List<SelectionListModel>();

        public int SelectedArableLandId { get; set; }

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            ArableLands = await SeedingService.GetAvailableArableLandSeeds(SelectedWorkingSeasonService.SelectedWorkingSeasonId);
        }

        protected async Task OnSubmit()
        {
            var response = await SeedingService.AddArableLand(new AddSeedingModel()
            {
                ArableLandId = SelectedArableLandId,
                WorkingSeasonId = SelectedWorkingSeasonService.SelectedWorkingSeasonId
            });

            DialogService.Close(response);
        }

    }
}
