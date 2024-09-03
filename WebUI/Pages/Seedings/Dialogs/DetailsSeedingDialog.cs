using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Seedings.Dialogs
{
    public partial class DetailsSeedingDialog
    {
        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int WorkingSeasonsId { get; set; }

        public List<SelectionListModel> ArableLands { get; set; } = new List<SelectionListModel>();

        public AddSeedingModel AddSeeding { get; set; } = new AddSeedingModel();

        public int SelectedArableLandId { get; set; }

        protected async override Task OnInitializedAsync()
        {
            ArableLands = await SeedingService.GetAvailableArableLandSeeds(WorkingSeasonsId);
            AddSeeding.WorkingSeasonId = WorkingSeasonsId;
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(AddSeedingModel addSeeding)
        {
            var response = await SeedingService.AddSeeding(addSeeding);

            DialogService.Close(response);
        }
    }
}
