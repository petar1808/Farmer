﻿using WebUI.ServicesModel.WorkingSeason;

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

        public async Task<List<ListWorkingSeasonModel>> List()
        {
            return await _httpService
                .GetAsync<List<ListWorkingSeasonModel>>($"api/workingSeasons");
        }

        public async Task<bool> Add(WorkingSeasonModel model)
        {
            return await _httpService
                .PostAsync<bool>("api/workingSeasons", model);
        }

        public async Task<bool> Update(WorkingSeasonModel model)
        {
            return await _httpService
                .PutAsync<bool>("api/workingSeasons", model);
        }

        public async Task<bool> Delete(int id)
        {
            return await _httpService.DeleteAsync<bool>($"api/workingSeasons/{id}");
        }
    }
}
