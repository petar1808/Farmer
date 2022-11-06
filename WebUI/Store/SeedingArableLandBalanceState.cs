using WebUI.ServicesModel.Seeding;

namespace WebUI.Store
{
    public class SeedingArableLandBalanceState
    {
        public GetArableLandBalanceModel ArableLandBalanceModelCurrentState { get; }

        public SeedingArableLandBalanceState(GetArableLandBalanceModel arableLandBalanceModelCurrentState)
        {
            ArableLandBalanceModelCurrentState = arableLandBalanceModelCurrentState;
        }
    }
}
