using WebUI.ServicesModel.ArableLand;

namespace WebUI.Services.ArableLand
{
    public interface IArableLandService
    {
        Task<List<ArableLandModel>> List();

        Task<ArableLandModel> Get(int id);

        Task<bool> Update(ArableLandModel arableLand);

        Task<bool> Add(ArableLandModel arableLand);

        Task<bool> Delete(int id);
    }
}
