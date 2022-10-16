using System.Text;
using System.Text.Json;
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

        public async Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId)
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/seeding/availableArableLands/{seasonId}");
        }

        public async Task<GetSeedingSummaryModel> GetSeedingSummary(int seedingId)
        {
            return await _httpService
                .GetAsync<GetSeedingSummaryModel>($"api/seeding/summary/{seedingId}");
        }

        public async Task<bool> UpdateSeedingSummary(SeedingSummaryBaseModel model, int seedingId)
        {
            return await _httpService
                .PutAsync<bool>($"api/seeding/summary/{seedingId}", model);
        }

        public async Task<List<SownArableLandModel>> GetSownArableLands(int seasonId)
        {
            return await _httpService
                .GetAsync<List<SownArableLandModel>>($"api/seeding/sownArableLand/{seasonId}");
        }
    }
}
