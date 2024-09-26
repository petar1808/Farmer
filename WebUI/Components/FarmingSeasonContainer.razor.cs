using Fluxor;
using Microsoft.AspNetCore.Components;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.Common;
using WebUI.Store.WorkingSeason;

namespace WebUI.Components
{
    public partial class FarmingSeasonContainer
    {
        [Parameter] 
        public RenderFragment ChildContent { get; set; }

        [Parameter] 
        public EventCallback OnSeasonChanged { get; set; }

        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedWorkingSeasonState { get; set; } = default!;

        public IEnumerable<SelectionListModel> FarmingSeasons { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {
            var seasons = await WorkingSeasonService.List();

            FarmingSeasons = seasons.Select(x => new SelectionListModel
            {
                Value = x.Id,
                Name = x.Name,
            });

            if (SelectedWorkingSeasonState.Value.WorkingSeasonId == 0)
            {
                Dispatcher.Dispatch(new UpdateSelectedWorkingSeasonState(FarmingSeasons.FirstOrDefault()?.Value ?? 0));
            }
            await OnSeasonChanged.InvokeAsync(); // Notify the parent
        }

        public async Task OnDropDownChangeTreatmentType(object value)
        {
            Dispatcher.Dispatch(new UpdateSelectedWorkingSeasonState((int)value));
            await OnSeasonChanged.InvokeAsync(); // Notify the parent
        }
    }
}
