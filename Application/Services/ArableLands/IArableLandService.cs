using Application.Models.ArableLands;
using Application.Models.Common;

namespace Application.Services.ArableLands
{
    public interface IArableLandService
    {
        Task Add(AddArableLandModel arableLandModel);

        Task Edit(EditArableLandModel arableLandModel);

        Task<GetAreableLandModel> Get(int id);

        Task<List<GetAreableLandModel>> List();

        Task Delete(int id);

        Task<List<SelectionListModel>> ArableLandsSelectionList(int seasionId);
    }
}
