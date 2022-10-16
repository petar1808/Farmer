using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Services.WorkingSeasons
{
    public class WorkingSeasonService : IWorkingSeasonService
    {
        private readonly IHttpService _httpService;
        public WorkingSeasonService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<WorkingSeasonModel> Get(int id)
        {
            return await _httpService
                .GetAsync<WorkingSeasonModel>($"api/workingSeasons/{id}");
        }

        public async Task<List<WorkingSeasonModel>> List()
        {
            return await _httpService
                .GetAsync<List<WorkingSeasonModel>>($"api/workingSeasons");
        }

        public async Task<bool> Add(WorkingSeasonModel workingSeason)
        {
            return await _httpService
                .PostAsync<bool>("api/workingSeasons", workingSeason);
        }

        public async Task<bool> Update(WorkingSeasonModel workingSeason)
        {
            return await _httpService
                .PutAsync<bool>("api/workingSeasons", workingSeason);
        }

        public async Task<bool> Delete(int id)
        {
           return await _httpService.DeleteAsync<bool>($"api/workingSeasons/{id}");
        }

        public async Task<List<SelectionListModel>> GetAllSeasons()
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/assets/seasons");
        }
    }
}
