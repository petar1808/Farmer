using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Services.PerformedWork
{
    public interface IPerformedWorkService
    {
        Task<List<GetPerformedWorkModel>> List(int seedingId);

        Task<PerformedWorkDatailsModel> Get(int id);

        Task Update(PerformedWorkDatailsModel editModel);

        Task Delete(int id);

        Task<bool> Add(PerformedWorkDatailsModel performedWork);

        Task<List<SelectionListModel>> GetWorkTypes();
    }
}
