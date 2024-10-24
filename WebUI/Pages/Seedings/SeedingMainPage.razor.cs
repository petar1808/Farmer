using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Extensions;
using WebUI.Pages.Work.Dialogs;
using WebUI.Services.PerformedWork;
using WebUI.Services.Seeding;
using WebUI.Services.Treatment;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;
using WebUI.Store;

namespace WebUI.Pages.Seedings
{
    public partial class SeedingMainPage
    {
        [Parameter]
        public int WorkingSeasonsId { get; set; }

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public NavigationManager UriHelper { get; set; } = default!;

        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public ITreatmentService TreatmentService { get; set; } = default!;

        [Inject]
        public IPerformedWorkService PerformedWorkService { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        [Inject]
        public IState<SeedingArableLandBalanceState> SeedingArableLandBalanceState { get; set; } = default!;

        public List<SelectionListModel> WorkingSeasons { get; set; } = new List<SelectionListModel>();

        public List<SownArableLandModel> SownArableLands { get; set; } = new List<SownArableLandModel>();

        public int SelectedSeedingId { get; set; }

        public bool CollapseSeedingSummary { get; set; }
        public bool CollapsePerformedWork { get; set; }
        public bool CollapseTreatment { get; set; }
        public bool CollapseSubsidy { get; set; }

        public bool ShowLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowLoading = true;
            SownArableLands = await SeedingService.GetSownArableLands(WorkingSeasonsId);

            if (SownArableLands.Any())
            {
                SelectedSeedingId = SownArableLands[0].SeedingId;
                await UpdateArableLandBalance(SelectedSeedingId);
            }
            ShowLoading = false;
        }

        public void BackToMainMenu()
        {
            this.Dispatcher.Dispatch(new UpdateSeedingArableLandBalance(new GetArableLandBalanceModel()));
            UriHelper.NavigateTo($"{UriHelper.BaseUri}workingSeason");
        }

        private async Task OnSelectedTabChanged(string seedingId)
        {
            if (SelectedSeedingId != int.Parse(seedingId))
            {
                SelectedSeedingId = int.Parse(seedingId);
                await UpdateArableLandBalance(SelectedSeedingId);
            }
        }

        public async Task AddArableLand()
        {
            var response = await DialogService.OpenAsync<DetailsSeedingDialog>($"Добавяне на Земя",
                parameters: new Dictionary<string, object>() { { "WorkingSeasonsId", WorkingSeasonsId } },
                options: DialogHelper.GetCommonDialogOptions());

            if (response == true)
            {
                SownArableLands = await SeedingService.GetSownArableLands(WorkingSeasonsId);
                SelectedSeedingId = SownArableLands.Last().SeedingId;
                this.StateHasChanged();
            }
        }

        private async Task UpdateArableLandBalance(int seedingId)
        {
            this.Dispatcher.Dispatch(
                new UpdateSeedingArableLandBalance(await SeedingService.GetArableLandBalance(seedingId))
                );
        }
    }
}
