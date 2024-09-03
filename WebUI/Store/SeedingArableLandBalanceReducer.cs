using Fluxor;

namespace WebUI.Store
{
    public class SeedingArableLandBalanceReducer : Reducer<SeedingArableLandBalanceState, UpdateSeedingArableLandBalance>
    {
        public override SeedingArableLandBalanceState Reduce(
            SeedingArableLandBalanceState state,
            UpdateSeedingArableLandBalance action)
        {
            return new SeedingArableLandBalanceState(action.ArableLandBalance);
        }
    }
}
