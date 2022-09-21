using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Services.Seeding
{
    public class SeedingService : ISeedingService
    {
        private readonly HttpClient _httpClient;
        public SeedingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddSeeding(AddSeedingModel seedingModel)
        {
            var seedingJson =
               new StringContent(JsonSerializer.Serialize(seedingModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/seeding", seedingJson);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId)
        {
            var result = await JsonSerializer.DeserializeAsync<List<SelectionListModel>>
                 (await _httpClient.GetStreamAsync($"api/seeding/availableArableLands/{seasonId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<GetSeedingSummaryModel> GetSeedingSummary(int seedingId)
        {
            var result = await JsonSerializer.DeserializeAsync<GetSeedingSummaryModel>
                  (await _httpClient.GetStreamAsync($"api/seeding/summary/{seedingId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<bool> UpdateSeedingSummary(SeedingSummaryBaseModel model, int seedingId)
        {
            var seedingSummaryJson =
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/seeding/summary/{seedingId}", seedingSummaryJson);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<SownArableLandModel>> GetSownArableLands(int seasonId)
        {
            var result = await JsonSerializer.DeserializeAsync<List<SownArableLandModel>>
                 (await _httpClient.GetStreamAsync($"api/seeding/sownArableLand/{seasonId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }
    }
}
