using WebUI.ServicesModel.Subsidies;

namespace WebUI.Services.Subsidies
{
    public class SubsidyService : ISubsidyService
    {
        private readonly IHttpService _httpService;

        public SubsidyService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> Add(SubsidiesModel subsidyModel)
        {
            return await _httpService
                .PostAsync<bool>($"api/seeding/subsidy", subsidyModel);
        }

        // Delete
        public async Task<bool> Add(SubsidiesModel subsidyModel, int seedingId)
        {
            return await _httpService
                .PostAsync<bool>($"api/seeding/{seedingId}/subsidy", subsidyModel);
        }

        public async Task<bool> Delete(int id)
        {
            return await _httpService
                .DeleteAsync<bool>($"api/seeding/subsidy/{id}");
        }

        public async Task<SubsidiesModel> Get(int id)
        {
            return await _httpService
                .GetAsync<SubsidiesModel>($"api/seeding/subsidy/{id}");
        }

        public async Task<List<SubsidiesModel>> ListBySeasonId(int seasonId)
        {
            return await _httpService
                    .GetAsync<List<SubsidiesModel>>($"/api/seeding/subsidy/list/{seasonId}");
        }

        public async Task<List<SubsidiesModel>> ListBySeedingId(int seedingId)
        {
            return await _httpService
                .GetAsync<List<SubsidiesModel>>($"api/seeding/{seedingId}/subsidy");
        }

        public async Task<bool> Update(SubsidiesModel subsidyModel)
        {
            return await _httpService
                .PutAsync<bool>("api/seeding/subsidy", subsidyModel);
        }
    }
}
