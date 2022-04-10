using Application.Models.ArableLands;

namespace Application.Services.ArableLands
{
    public interface IArableLandService
    {
        Task Add(string name, int sizeInDecar);

        Task Edit(int id, string name, int sizeInDecar);

        Task<GetAreableLandModel> Get(int id);

        Task<List<GetAreableLandModel>> GetAll();

        Task Delete(int id);
    }
}
