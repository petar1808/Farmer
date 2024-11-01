using Fluxor;

namespace WebUI.Store.WorkingSeason
{
    public class SelectedWorkingSeasonReducer : Reducer<SelectedWorkingSeasonState, UpdateSelectedWorkingSeasonState>
    {
        public override SelectedWorkingSeasonState Reduce(
            SelectedWorkingSeasonState state,
            UpdateSelectedWorkingSeasonState action)
        {
            return new SelectedWorkingSeasonState(action.WorkingSeasonId, action.Name);
        }
    }
}
