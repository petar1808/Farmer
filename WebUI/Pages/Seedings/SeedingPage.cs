using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services;
using WebUI.Services.Seeding;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Seedings
{
    public partial class SeedingPage
    {
        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public SelectedWorkingSeasonService SelectedWorkingSeasonService { get; set; } = default!;

        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        public List<SelectionListModel> WorkingSeasons { get; set; } = new List<SelectionListModel>();

        public List<SownArableLandModel> SownArableLands { get; set; } = new List<SownArableLandModel>();

        int selectedSeedingId = 0;

        public bool CollapseSeedingSummary { get; set; } = true;
        public bool CollapsePerformedWork { get; set; }
        public bool CollapseTreatment { get; set; }

        protected override async Task OnInitializedAsync()
        {
            WorkingSeasons = await WorkingSeasonService.GetAllSeasons();

            if (SelectedWorkingSeasonService.IsDefaultValue())
            {
                SelectedWorkingSeasonService.ChangeSelectedWorkingSeason(WorkingSeasons.OrderByDescending(x => x.Value).First()?.Value ?? 0);
            }

            SownArableLands = await SeedingService.GetSownArableLands(SelectedWorkingSeasonService.SelectedWorkingSeasonId);

            if (SownArableLands.Any())
            {
                selectedSeedingId = SownArableLands[0].SeedingId;
            }

        }

        void OnChange(int index)
        {
            selectedSeedingId = SownArableLands[index].SeedingId;
        }

        public async Task AddArableLand()
        {
            var response = await DialogService.OpenAsync<DetailsSeedingDialog>($"Земя",
              options: new DialogOptions() { Width = "750px", Height = "470px" });

            SownArableLands = await SeedingService.GetSownArableLands(SelectedWorkingSeasonService.SelectedWorkingSeasonId);

            this.StateHasChanged();
        }

        public async Task OnDropDownChange(object value)
        {
            SelectedWorkingSeasonService.ChangeSelectedWorkingSeason((int)value);

            SownArableLands = await SeedingService.GetSownArableLands((int)value);

            this.StateHasChanged();
        }
    }
}
