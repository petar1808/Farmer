using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Services.Seeding
{
    public class SeedingService : ISeedingService
    {
        private readonly IHttpService _httpService;
        public SeedingService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> AddSeeding(AddSeedingModel seedingModel)
        {
            return await _httpService
                .PostAsync<bool>("api/seeding", seedingModel);
        }

        public async Task<List<ListSeedingModel>> ListSeeding(int seasonId)
        {
            return await _httpService
                .GetAsync<List<ListSeedingModel>>($"api/seeding/{seasonId}");
        }

        public async Task<List<SelectionListModel>> ListSeedingSelectionList()
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/seeding");
        }

        public async Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId)
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/seeding/availableArableLands/{seasonId}");
        }

        public async Task<DetailsSowingModel> GetSowingDetails(int seedingId)
        {
            return await _httpService
                .GetAsync<DetailsSowingModel>($"api/seeding/summary/{seedingId}");
        }

        public async Task<List<SownArableLandModel>> GetSownArableLands(int seasonId)
        {
            return await _httpService
                .GetAsync<List<SownArableLandModel>>($"api/seeding/sownArableLand/{seasonId}");
        }

        public async Task<bool> UpdateSeedingSummaryNew(DetailsSowingModel model, int seedingId)
        {
            return await _httpService
                    .PutAsync<bool>($"api/seeding/summary/{seedingId}", model);
        }
    }
}
