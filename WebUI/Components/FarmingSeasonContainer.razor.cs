using Fluxor;
using Microsoft.AspNetCore.Components;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.WorkingSeason;
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

        public IEnumerable<SelectionListModel> FarmingSeasonsDropDownData { get; set; } = new List<SelectionListModel>();

        public int SelectedSeasonId { get; set; }

        public List<WorkingSeasonModel> WorkingSeasons { get; set; } = new List<WorkingSeasonModel>();

        protected async override Task OnInitializedAsync()
        {
            WorkingSeasons = await WorkingSeasonService.List();

            FarmingSeasonsDropDownData = WorkingSeasons.Select(x => new SelectionListModel
            {
                Value = x.Id,
                Name = x.Name,
            });

            if (SelectedWorkingSeasonState.Value.WorkingSeasonId == 0)
            {
                var season = WorkingSeasons.FirstOrDefault();
                SelectedSeasonId = season?.Id ?? default;

                Dispatcher.Dispatch(
                    new UpdateSelectedWorkingSeasonState(season?.Id ?? default, season?.Name ?? string.Empty));
            }
            else
            {
                SelectedSeasonId = SelectedWorkingSeasonState.Value.WorkingSeasonId;
            }

            if (OnSeasonChanged.HasDelegate)
            {
                await OnSeasonChanged.InvokeAsync();
            }
        }

        public async Task OnDropDownChange(object value)
        {
            var workingSeason = WorkingSeasons.SingleOrDefault(c => c.Id == (int)value);
            Dispatcher.Dispatch(
                new UpdateSelectedWorkingSeasonState(workingSeason?.Id ?? default, workingSeason?.Name ?? string.Empty));

            if (OnSeasonChanged.HasDelegate)
            {
                await OnSeasonChanged.InvokeAsync();
            }
        }
    }
}
