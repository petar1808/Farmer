using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Services.Seeding
{
    public interface ISeedingService
    {
        Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId);

        Task<bool> AddSeeding(AddSeedingModel seedingModel);

        Task<List<SownArableLandModel>> GetSownArableLands(int seasonId);

        Task<GetSeedingSummaryModel> GetSeedingSummary(int seedingId);

        Task<bool> UpdateSeedingSummary(SeedingSummaryBaseModel model, int seedingId);
    }
}
