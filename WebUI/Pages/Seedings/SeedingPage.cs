using Blazorise;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Services;
using WebUI.Services.PerformedWork;
using WebUI.Services.Seeding;
using WebUI.Services.Treatment;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;
using WebUI.ServicesModel.Seeding;
using WebUI.ServicesModel.Тreatment;

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

        [Inject]
        public ITreatmentService TreatmentService { get; set; } = default!;

        [Inject]
        public IPerformedWorkService PerformedWorkService { get; set; } = default!;

        public List<SelectionListModel> WorkingSeasons { get; set; } = new List<SelectionListModel>();

        public List<SownArableLandModel> SownArableLands { get; set; } = new List<SownArableLandModel>();

        public GetSeedingSummaryModel SeedingSummaryData { get; set; } = default!;

        int selectedSeedingId = 0;

        public bool CollapseSeedingSummary { get; set; }
        public bool CollapsePerformedWork { get; set; }
        public bool CollapseTreatment { get; set; }

        protected override async Task OnInitializedAsync()
        {
            WorkingSeasons = await WorkingSeasonService.GetAllSeasons();

            if (SelectedWorkingSeasonService.IsDefaultValue())
            {
                SelectedWorkingSeasonService
                    .ChangeSelectedWorkingSeason(WorkingSeasons.OrderByDescending(x => x.Name).First()?.Value ?? 0);
            }

            SownArableLands = await SeedingService
                .GetSownArableLands(SelectedWorkingSeasonService.SelectedWorkingSeasonId);

            if (SownArableLands.Any())
            {
                selectedSeedingId = SownArableLands[0].SeedingId;
            }
        }

        async void OnTabChange(int index)
        {
            selectedSeedingId = SownArableLands[index].SeedingId;
            if (this.CollapseSeedingSummary)
            {
                await LoadSeedingSummary(selectedSeedingId);
            }
            else if (!this.CollapseSeedingSummary)
            {
                this.SeedingSummaryData = default!;
            }

            //if (this.CollapseTreatment)
            //{
            //    await LoadTreatmentData(selectedSeedingId);
            //}
            //else if (!this.CollapseTreatment)
            //{
            //    this.TreatmentData = default!;
            //}

            //if (this.CollapsePerformedWork)
            //{
            //    await LoadPerformedWorkData(selectedSeedingId);
            //}
            //else if (!this.CollapsePerformedWork)
            //{
            //    this.TreatmentData = default!;
            //}
            this.StateHasChanged();
        }

        public async Task AddArableLand()
        {
            var response = await DialogService.OpenAsync<DetailsSeedingDialog>($"Добавяне на Земя",
              options: new DialogOptions() { Width = "500px", Height = "220px" });

            SownArableLands = await SeedingService
                .GetSownArableLands(SelectedWorkingSeasonService.SelectedWorkingSeasonId);

            this.StateHasChanged();
        }

        public async Task OnDropDownChange(object value)
        {
            SelectedWorkingSeasonService.ChangeSelectedWorkingSeason((int)value);

            SownArableLands = await SeedingService.GetSownArableLands((int)value);

            this.CollapseSeedingSummary = false;
            this.SeedingSummaryData = default!;

            this.CollapsePerformedWork = false;
            //this.PerformedWorkData = default!;

            this.CollapseTreatment = false;
            //this.TreatmentData = default!;

            this.StateHasChanged();
        }

        //public async void OnTreatmentCollapse(int seedingId)
        //{
        //    //if (!this.CollapseTreatment && this.TreatmentData == null)
        //    //{
        //    //    await LoadTreatmentData(seedingId);
        //    //}

        //    this.CollapseTreatment = !this.CollapseTreatment;
        //}

        //private async Task LoadTreatmentData(int seedingId)
        //{
        //    this.TreatmentData = await this.TreatmentService.List(seedingId);
        //}

        public async Task OnSeedingSummaryCollapse(int seedingId)
        {
            if (!this.CollapseSeedingSummary && this.SeedingSummaryData == null)
            {
                await LoadSeedingSummary(seedingId);
            }

            this.CollapseSeedingSummary = !this.CollapseSeedingSummary;
        }

        private async Task LoadSeedingSummary(int seedingId)
        {
            this.SeedingSummaryData = await this.SeedingService.GetSeedingSummary(seedingId);
        }

        //public async Task OnPerformedWokrCollapse(int seedingId)
        //{
        //    //if (!this.CollapsePerformedWork && this.PerformedWorkData == null)
        //    //{
        //    //    await LoadPerformedWorkData(seedingId);
        //    //}

        //    this.CollapsePerformedWork = !this.CollapsePerformedWork;
        //}

        //private async Task LoadPerformedWorkData(int seedingId)
        //{
        //    this.PerformedWorkData = await this.PerformedWorkService.List(seedingId);
        //}
    }
}
