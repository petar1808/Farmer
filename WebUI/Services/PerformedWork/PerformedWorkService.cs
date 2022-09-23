using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Services.PerformedWork
{
    public class PerformedWorkService : IPerformedWorkService
    {
        private readonly HttpClient _httpClient;
        public PerformedWorkService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Add(PerformedWorkDatailsModel performedWork, int seedingId)
        {
            var performedWorkJson =
                new StringContent(JsonSerializer.Serialize(performedWork), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/seeding/{seedingId}/performedWork", performedWorkJson);

            return response.IsSuccessStatusCode;
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/seeding/performedWork/{id}");
        }

        public async Task<List<GetPerformedWorkModel>> List(int seedingId)
        {
            var result = await JsonSerializer.DeserializeAsync<List<GetPerformedWorkModel>>
                (await _httpClient.GetStreamAsync($"api/seeding/{seedingId}/performedWork"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

             return result!;
        }

        public async Task<List<SelectionListModel>> GetWorkTypes()
        {
            var result = await JsonSerializer.DeserializeAsync<List<SelectionListModel>>
              (await _httpClient.GetStreamAsync($"api/assets/workTypes"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task Update(PerformedWorkDatailsModel editModel)
        {
            var performedWorkJson =
                new StringContent(JsonSerializer.Serialize(editModel), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/seeding/performedWork", performedWorkJson);
        }

        public async Task<PerformedWorkDatailsModel> Get(int id)
        {
            var result = await JsonSerializer.DeserializeAsync<PerformedWorkDatailsModel>
                 (await _httpClient.GetStreamAsync($"api/seeding/performedWork/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }
    }
}
