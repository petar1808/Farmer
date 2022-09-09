using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Services.Seeding
{
    public interface ISeedingService
    {
        Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId);

        Task<bool> AddArableLand(AddSeedingModel seedingModel);

        Task<List<SownArableLandModel>> GetSownArableLands(int seasonId);

        Task<GetSeedingModel> GetSeeding(int seasonId, int arableLandId);
    }
}
