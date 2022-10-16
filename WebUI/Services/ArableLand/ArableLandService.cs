using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.ArableLand;

namespace WebUI.Services.ArableLand
{
    public class ArableLandService : IArableLandService
    {
        private readonly IHttpService _httpService;
        public ArableLandService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ArableLandModel> Get(int id)
        {
            return await _httpService
                .GetAsync<ArableLandModel>($"api/arableLands/{id}");
        }

        public async Task<List<ArableLandModel>> List()
        {
            return await _httpService
                .GetAsync<List<ArableLandModel>>($"api/arableLands");
        }

        public async Task<bool> Add(ArableLandModel arableLand)
        {
            return await _httpService
                .PostAsync<bool>("api/arableLands", arableLand);
        }

        public async Task<bool> Update(ArableLandModel arableLand)
        {
            return await _httpService
                .PutAsync<bool>("api/arableLands", arableLand);
        }

        public async Task<bool> Delete(int id)
        {
            return await _httpService
                .DeleteAsync<bool>($"api/arableLands/{id}");
        }
    }
}
