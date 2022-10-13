using Application.Models;
using Application.Models.ArableLands;
using Application.Models.Common;

namespace Application.Services.ArableLands
{
    public interface IArableLandService
    {
        Task<Result> Add(AddArableLandModel arableLandModel);

        Task<Result> Edit(EditArableLandModel arableLandModel);

        Task<Result<GetAreableLandModel>> Get(int id);

        Task<Result<List<GetAreableLandModel>>> List();

        Task<Result> Delete(int id);

        Task<Result<List<SelectionListModel>>> ArableLandsSelectionList(int seasonId);
    }
}
