namespace WebUI.Services.Subsidies
{
    public interface ISubsidyService
    {
        Task<List<SubsidiesModel>> List(int seedingId);

        Task<SubsidiesModel> Get(int id);

        Task<bool> Delete(int id);

        Task<bool> Update(SubsidiesModel subsidyModel);

        Task<bool> Add(SubsidiesModel subsidyModel, int seedingId);
    }
}
