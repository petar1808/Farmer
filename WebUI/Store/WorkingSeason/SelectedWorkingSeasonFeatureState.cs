using Fluxor;

namespace WebUI.Store.WorkingSeason
{
    public class SelectedWorkingSeasonFeatureState : Feature<SelectedWorkingSeasonState>
    {
        public override string GetName() => nameof(SelectedWorkingSeasonFeatureState);

        protected override SelectedWorkingSeasonState GetInitialState() => new SelectedWorkingSeasonState(0);
    }
}
