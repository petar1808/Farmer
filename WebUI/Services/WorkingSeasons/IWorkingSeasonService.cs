﻿using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Services.WorkingSeasons
{
    public interface IWorkingSeasonService
    {
        Task<List<ListWorkingSeasonModel>> List();

        Task<WorkingSeasonModel> Get(int id);

        Task<bool> Update(WorkingSeasonModel arableLand);

        Task<bool> Add(WorkingSeasonModel arableLand);

        Task<bool> Delete(int id);
    }
}
