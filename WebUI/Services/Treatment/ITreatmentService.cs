using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;
using WebUI.ServicesModel.Тreatment;

namespace WebUI.Services.Treatment
{
    public interface ITreatmentService
    {
        Task<List<GetTreatmentModel>> List(int seedingId);

        Task<ТreatmentDetailsModel> Get(int id);

        Task<bool> Update(ТreatmentDetailsModel editModel);

        Task<bool> Delete(int id);

        Task<bool> Add(ТreatmentDetailsModel treatment, int seedingId);

        Task<List<SelectionListModel>> GetTreatmentTypes();
    }
}
