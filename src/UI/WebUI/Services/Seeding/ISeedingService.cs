using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Services.Seeding
{
    public interface ISeedingService
    {
        Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId);

        Task<bool> AddSeeding(AddSeedingModel seedingModel);

        Task<List<ListSeedingModel>> ListSeeding(int seasonId);

        Task<List<SelectionListModel>> ListSeedingSelectionList();

        Task<List<SownArableLandModel>> GetSownArableLands(int seasonId);

        Task<DetailsSowingModel> GetSowingDetails(int seedingId);
     
        Task<bool> UpdateSeedingSummaryNew(DetailsSowingModel model, int seedingId);
    }
}
