using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Services.PerformedWork
{
    public class PerformedWorkService : IPerformedWorkService
    {
        private readonly IHttpService _httpService;
        public PerformedWorkService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> Add(PerformedWorkDatailsModel performedWork, int seedingId)
        {
            return await _httpService
                .PostAsync<bool>($"api/seeding/{seedingId}/performedWork", performedWork);
        }

        public async Task<bool> Delete(int id)
        {
            return await _httpService
                .DeleteAsync<bool>($"api/seeding/performedWork/{id}");
        }

        public async Task<List<ListPerformedWorkModel>> List(int seedingId)
        {
            return await _httpService
                .GetAsync<List<ListPerformedWorkModel>>($"api/seeding/{seedingId}/performedWork");
        }

        public async Task<List<SelectionListModel>> GetWorkTypes()
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/assets/workTypes");
        }

        public async Task<bool> Update(PerformedWorkDatailsModel editModel)
        {
            return await _httpService
                .PutAsync<bool>("api/seeding/performedWork", editModel);
        }

        public async Task<PerformedWorkDatailsModel> Get(int id)
        {
            return await _httpService
                .GetAsync<PerformedWorkDatailsModel>($"api/seeding/performedWork/{id}");
        }
    }
}
