using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Services.WorkingSeasons
{
    public interface IWorkingSeasonService
    {
        Task<List<GetWorkingSeasonApiModel>> List();

        Task<GetWorkingSeasonApiModel> Get(int id);

        Task Update(GetWorkingSeasonApiModel arableLand);

        Task<bool> Add(GetWorkingSeasonApiModel arableLand);

        Task Delete(int id);
    }
}
