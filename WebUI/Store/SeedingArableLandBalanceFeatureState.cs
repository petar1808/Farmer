using Fluxor;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Store
{
    public class SeedingArableLandBalanceFeatureState : Feature<SeedingArableLandBalanceState>
    {
        public override string GetName()
        {
            return nameof(SeedingArableLandBalanceState);
        }

        protected override SeedingArableLandBalanceState GetInitialState()
        {
            return new SeedingArableLandBalanceState(new GetArableLandBalanceModel());
        }
    }
}
