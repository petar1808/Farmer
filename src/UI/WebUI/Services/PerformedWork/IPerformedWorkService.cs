using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Services.PerformedWork
{
    public interface IPerformedWorkService
    {
        Task<List<ListPerformedWorkModel>> List(int seedingId);

        Task<PerformedWorkDatailsModel> Get(int id);

        Task<bool> Update(PerformedWorkDatailsModel editModel);

        Task<bool> Delete(int id);

        Task<bool> Add(PerformedWorkDatailsModel performedWork, int seedingId);

        Task<List<SelectionListModel>> GetWorkTypes();
    }
}
