using Application.Models.ArableLands;
using Application.Models.Common;

namespace Application.Services.ArableLands
{
    public interface IArableLandService
    {
        Task Add(string name, int sizeInDecar);

        Task Edit(int id, string name, int sizeInDecar);

        Task<GetAreableLandModel> Get(int id);

        Task<List<GetAreableLandModel>> GetAll();

        Task Delete(int id);

        Task<List<SelectionListModel>> ArableLandsSelectionList(int seasionId, int? currentArableLandId = null);
    }
}
