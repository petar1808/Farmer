using System.Text.Json;
using WebUI.ServicesModel.Identity;

namespace WebUI.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        public IdentityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ListUserModel>> ListUser()
        {
            var result = await JsonSerializer.DeserializeAsync<List<ListUserModel>>
                (await _httpClient.GetStreamAsync($"api/identity/listUser"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }
    }
}
