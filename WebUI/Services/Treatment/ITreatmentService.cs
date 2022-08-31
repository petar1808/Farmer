using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;
using WebUI.ServicesModel.Тreatment;

namespace WebUI.Services.Treatment
{
    public interface ITreatmentService
    {
        Task<List<GetTreatmentModel>> List(int seedingId);

        Task<ТreatmentDetailsModel> Get(int id);

        Task Update(ТreatmentDetailsModel editModel);

        Task Delete(int id);

        Task<bool> Add(ТreatmentDetailsModel тreatment, int seedingId);

        Task<List<SelectionListModel>> GetTreatmentTypes();
    }
}
