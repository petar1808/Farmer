using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.Common;
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

        public async Task<WorkingSeasonModel> Get(int id)
        {
            var result = await JsonSerializer.DeserializeAsync<WorkingSeasonModel>
                (await _httpClient.GetStreamAsync($"api/workingSeasons/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<List<WorkingSeasonModel>> List()
        {

            var result = await JsonSerializer.DeserializeAsync<List<WorkingSeasonModel>>
                (await _httpClient.GetStreamAsync($"api/workingSeasons"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<bool> Add(WorkingSeasonModel workingSeason)
        {
            var workingSeasonsJson =
                new StringContent(JsonSerializer.Serialize(workingSeason), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/workingSeasons", workingSeasonsJson);

            return response.IsSuccessStatusCode;
        }

        public async Task Update(WorkingSeasonModel workingSeason)
        {
            var workingSeasonsJson =
                new StringContent(JsonSerializer.Serialize(workingSeason), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/workingSeasons", workingSeasonsJson);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/workingSeasons/{id}");
        }

        public async Task<List<SelectionListModel>> GetAllSeasons()
        {
            var result = await JsonSerializer.DeserializeAsync<List<SelectionListModel>>
               (await _httpClient.GetStreamAsync($"api/workingSeasons/allSeasons"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }
    }
}
