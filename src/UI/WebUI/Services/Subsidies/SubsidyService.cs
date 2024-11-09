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

        public async Task<bool> Add(DetailsSubsidyModel subsidyModel)
        {
            return await _httpService
                .PostAsync<bool>($"api/subsidy", subsidyModel);
        }

        public async Task<bool> Delete(int id)
        {
            return await _httpService
                .DeleteAsync<bool>($"api/subsidy/{id}");
        }

        public async Task<DetailsSubsidyModel> Get(int id)
        {
            return await _httpService
                .GetAsync<DetailsSubsidyModel>($"api/subsidy/{id}");
        }

        public async Task<List<ListSubsidiesModel>> List(int seasonId)
        {
            return await _httpService
                    .GetAsync<List<ListSubsidiesModel>>($"/api/subsidy/list/{seasonId}");
        }

        public async Task<bool> Update(DetailsSubsidyModel subsidyModel)
        {
            return await _httpService
                .PutAsync<bool>("api/subsidy", subsidyModel);
        }
    }
}
