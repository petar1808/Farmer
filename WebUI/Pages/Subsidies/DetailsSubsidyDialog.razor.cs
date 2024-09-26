using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Seeding;
using WebUI.Services.Subsidies;
using WebUI.ServicesModel.Seeding;
using WebUI.ServicesModel.Subsidies;
using WebUI.Store.WorkingSeason;

namespace WebUI.Pages.Subsidies
{
    public partial class DetailsSubsidyDialog
    {
        [Inject]
        public ISubsidyService SubsidyService { get; set; } = default!;

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public List<SownArableLandModel> SownArableLands { get; set; } = new List<SownArableLandModel>();

        public IList<int> SelectedValues { get; set; } = new List<int>();

        [Parameter]
        public int WorkingSeasonId { get; set; }

        public SubsidiesModel Subsidies { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedFarmingSeasonId { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            SownArableLands = await SeedingService.GetSownArableLands(1);

            SelectedValues = SownArableLands.Select(x => x.SeedingId).ToList();

            if (WorkingSeasonId == 0)
            {
                Subsidies = new SubsidiesModel();
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(SubsidiesModel subsidiesModel)
        {
            subsidiesModel.SeasonId = SelectedFarmingSeasonId.Value.WorkingSeasonId;
            subsidiesModel.ArableLandIds = SelectedValues;
            bool addIsSuccess = false;

            if (subsidiesModel.Id == 0)
            {
                addIsSuccess = await SubsidyService.Add(Subsidies);
            }
            //else
            //{
            //    addIsSuccess = await SubsidyService.Update(Subsidies);
            //}
            DialogService.Close(addIsSuccess);
        }
    }
}
