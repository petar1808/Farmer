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

        [Parameter]
        public int WorkingSeasonId { get; set; }

        [Parameter]
        public int SubsidyId { get; set; }

        public List<SownArableLandModel> SownArableLands { get; set; } = new List<SownArableLandModel>();

        public DetailsSubsidyModel Subsidy { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedFarmingSeasonId { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            SownArableLands = await SeedingService.GetSownArableLands(WorkingSeasonId);

            if (SubsidyId == 0)
            {
                Subsidy = new DetailsSubsidyModel();
                Subsidy.SelectedArableLands = SownArableLands.Select(x => x.ArableLandId).ToList();
            }
            else
            {
                Subsidy = await SubsidyService.Get(SubsidyId);
                Subsidy.SelectedArableLands = Subsidy.SelectedArableLands.ToList();
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(DetailsSubsidyModel subsidiesModel)
        {
            bool isSuccess = false;

            if (subsidiesModel.Id == 0)
            {
                subsidiesModel.SeasonId = WorkingSeasonId;
                isSuccess = await SubsidyService.Add(subsidiesModel);
            }
            else
            {
                isSuccess = await SubsidyService.Update(subsidiesModel);
            }
            DialogService.Close(isSuccess);
        }
    }
}
