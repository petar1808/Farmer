using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Services.WorkingSeasons
{
    public interface IWorkingSeasonService
    {
        Task<List<WorkingSeasonModel>> List();

        Task<WorkingSeasonModel> Get(int id);

        Task Update(WorkingSeasonModel arableLand);

        Task<bool> Add(WorkingSeasonModel arableLand);

        Task Delete(int id);
    }
}
