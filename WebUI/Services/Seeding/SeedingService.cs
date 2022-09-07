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

        public async Task<bool> AddArableLand(AddSeedingModel seedingModel)
        {
            var seedingJson =
               new StringContent(JsonSerializer.Serialize(seedingModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/seedings", seedingJson);

            return response.IsSuccessStatusCode;
        }

        public async Task<List<SelectionListModel>> GetAvailableArableLandSeeds(int seasonId)
        {
            var result = await JsonSerializer.DeserializeAsync<List<SelectionListModel>>
                 (await _httpClient.GetStreamAsync($"api/seedings/{seasonId}/arableLand"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }
    }
}
