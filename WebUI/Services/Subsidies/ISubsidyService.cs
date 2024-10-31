using WebUI.ServicesModel.Subsidies;

namespace WebUI.Services.Subsidies
{
    public interface ISubsidyService
    {
        Task<bool> Add(DetailsSubsidyModel subsidyModel);

        Task<bool> Update(DetailsSubsidyModel subsidyModel);

        Task<DetailsSubsidyModel> Get(int id);

        Task<List<ListSubsidiesModel>> List(int seasonId);

        Task<bool> Delete(int id);
    }
}
