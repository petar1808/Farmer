using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Services.Seeding
{
    public interface ISeedingService
    {
        Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId);

        Task<bool> AddSeeding(AddSeedingModel seedingModel);

        Task<List<ListSeedingModel>> ListSeeding(int seasonId);

        Task<List<SownArableLandModel>> GetSownArableLands(int seasonId);

        // Delete
        Task<GetSeedingSummaryModel> GetSeedingSummary(int seedingId);

        Task<DetailsSowingModel> GetSowingDetails(int seedingId);

        // Delete
        Task<bool> UpdateSeedingSummary(SeedingSummaryBaseModel model, int seedingId);
     
        Task<bool> UpdateSeedingSummaryNew(DetailsSowingModel model, int seedingId);

        Task<GetArableLandBalanceModel> GetArableLandBalance(int seedingId);
    }
}
