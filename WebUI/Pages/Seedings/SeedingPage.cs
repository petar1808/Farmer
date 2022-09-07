using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services;
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

        public List<SelectionListModel> WorkingSeasons { get; set; } = new List<SelectionListModel>();

        protected override async Task OnInitializedAsync()
        {
            WorkingSeasons = await WorkingSeasonService.GetAllSeasons();

            if (SelectedWorkingSeasonService.IsDefaultValue())
            {
                SelectedWorkingSeasonService.ChangeSelectedWorkingSeason(WorkingSeasons.OrderByDescending(x => x.Value).First()?.Value ?? 0);
            }
        }

        public async Task AddArableLand()
        {
            var response = await DialogService.OpenAsync<DetailsSeeding>($"Земя",
              options: new DialogOptions() { Width = "750px", Height = "470px" });

            this.StateHasChanged();
        }

        public void OnDropDownChange(object value)
        {
            SelectedWorkingSeasonService.ChangeSelectedWorkingSeason((int)value);
        }
    }
}
