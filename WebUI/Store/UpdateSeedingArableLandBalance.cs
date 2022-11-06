using WebUI.ServicesModel.Seeding;

namespace WebUI.Store
{
    public class UpdateSeedingArableLandBalance
    {
        public GetArableLandBalanceModel ArableLandBalance { get; set; }

        public UpdateSeedingArableLandBalance(GetArableLandBalanceModel arableLandBalance)
        {
            ArableLandBalance = arableLandBalance;
        }
    }
}
