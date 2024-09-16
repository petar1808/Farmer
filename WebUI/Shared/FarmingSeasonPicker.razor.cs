using Microsoft.AspNetCore.Components;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.Common;

namespace WebUI.Shared
{
    public partial class FarmingSeasonPicker
    {
        public IEnumerable<SelectionListModel> FarmingSeasons { get; set; } = new List<SelectionListModel>();

        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Parameter] public bool Visible { get; set; } = false;

        public int SelectedFarmingSeasonId { get; set; }
        protected async override Task OnInitializedAsync()
        {
            var seasons = await WorkingSeasonService.List();

            FarmingSeasons = seasons.Select(x => new SelectionListModel
            {
                Value = x.Id,
                Name = x.Name,
            });

            SelectedFarmingSeasonId = FarmingSeasons.FirstOrDefault()?.Value ?? 0;
        }

        public async Task OnDropDownChangeTreatmentType(object value)
        {
            int parse = (int)value;
        }
    }
}
