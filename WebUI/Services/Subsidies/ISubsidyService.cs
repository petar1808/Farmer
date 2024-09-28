using WebUI.ServicesModel.Subsidies;

namespace WebUI.Services.Subsidies
{
    public interface ISubsidyService
    {
        Task<List<SubsidiesModel>> ListBySeedingId(int seedingId);

        Task<List<SubsidiesModel>> ListBySeasonId(int seasonId);

        Task<SubsidiesModel> Get(int id);

        Task<bool> Delete(int id);

        Task<bool> Update(SubsidiesModel subsidyModel);

        Task<bool> Add(SubsidiesModel subsidyModel);
    }
}
