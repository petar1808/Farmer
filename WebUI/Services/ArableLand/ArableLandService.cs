using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.ArableLand;

namespace WebUI.Services.ArableLand
{
    public class ArableLandService : IArableLandService
    {

        private readonly HttpClient _httpClient;
        public ArableLandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ArableLandModel> Get(int id)
        {
            var result = await JsonSerializer.DeserializeAsync<ArableLandModel>
                (await _httpClient.GetStreamAsync($"api/arableLands/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<List<ArableLandModel>> List()
        {

            var result = await JsonSerializer.DeserializeAsync<List<ArableLandModel>>
                (await _httpClient.GetStreamAsync($"api/arableLands"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<bool> Add(ArableLandModel arableLand)
        {
            var arableLandJson =
                new StringContent(JsonSerializer.Serialize(arableLand), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/arableLands", arableLandJson);

            return response.IsSuccessStatusCode;
        }

        public async Task Update(ArableLandModel arableLand)
        {
            var arableLandJson =
                new StringContent(JsonSerializer.Serialize(arableLand), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/arableLands", arableLandJson);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/arableLands/{id}");
        }
    }
}
