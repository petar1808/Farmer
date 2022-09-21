using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Тreatment;

namespace WebUI.Services.Treatment
{
    public class TreatmentService : ITreatmentService
    {
        private readonly HttpClient _httpClient;
        public TreatmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> Add(ТreatmentDetailsModel тreatment, int seedingId)
        {
            var treatmentJson =
                new StringContent(JsonSerializer.Serialize(тreatment), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/seeding/{seedingId}/treatment", treatmentJson);

            return response.IsSuccessStatusCode;
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/seeding/{id}/treatment");
        }

        public async Task<ТreatmentDetailsModel> Get(int id)
        {
            var result = await JsonSerializer.DeserializeAsync<ТreatmentDetailsModel>
                (await _httpClient.GetStreamAsync($"api/seeding/{id}/getTreatment"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<List<SelectionListModel>> GetTreatmentTypes()
        {
            var result = await JsonSerializer.DeserializeAsync<List<SelectionListModel>>
             (await _httpClient.GetStreamAsync($"api/assets/treatmentType"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<List<GetTreatmentModel>> List(int seedingId)
        {
            var result = await JsonSerializer.DeserializeAsync<List<GetTreatmentModel>>
               (await _httpClient.GetStreamAsync($"api/seeding/{seedingId}/treatment"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task Update(ТreatmentDetailsModel editModel)
        {
            var treatmentJson =
                new StringContent(JsonSerializer.Serialize(editModel), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/seeding/treatment", treatmentJson);
        }
    }
}
