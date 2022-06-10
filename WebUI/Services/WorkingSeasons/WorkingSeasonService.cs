using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Services.WorkingSeasons
{
    public class WorkingSeasonService : IWorkingSeasonService
    {

        private readonly HttpClient _httpClient;
        public WorkingSeasonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetWorkingSeasonApiModel> Get(int id)
        {
            var result = await JsonSerializer.DeserializeAsync<GetWorkingSeasonApiModel>
                (await _httpClient.GetStreamAsync($"api/workingSeasons/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<List<GetWorkingSeasonApiModel>> List()
        {

            var result = await JsonSerializer.DeserializeAsync<List<GetWorkingSeasonApiModel>>
                (await _httpClient.GetStreamAsync($"api/workingSeasons"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<bool> Add(GetWorkingSeasonApiModel workingSeason)
        {
            var workingSeasonsJson =
                new StringContent(JsonSerializer.Serialize(workingSeason), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/workingSeasons", workingSeasonsJson);

            return response.IsSuccessStatusCode;
        }

        public async Task Update(GetWorkingSeasonApiModel workingSeason)
        {
            var workingSeasonsJson =
                new StringContent(JsonSerializer.Serialize(workingSeason), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/workingSeasons", workingSeasonsJson);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/workingSeasons/{id}");
        }
    }
}
