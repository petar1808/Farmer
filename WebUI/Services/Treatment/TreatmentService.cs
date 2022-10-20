using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Тreatment;

namespace WebUI.Services.Treatment
{
    public class TreatmentService : ITreatmentService
    {
        private readonly IHttpService _httpService;
        public TreatmentService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<bool> Add(ТreatmentDetailsModel treatment, int seedingId)
        {
            return await _httpService
                .PostAsync<bool>($"api/seeding/{seedingId}/treatment", treatment);
        }

        public async Task<bool> Delete(int id)
        {
           return await _httpService
                .DeleteAsync <bool>($"api/seeding/treatment/{id}");
        }

        public async Task<ТreatmentDetailsModel> Get(int id)
        {
            return await _httpService
                .GetAsync<ТreatmentDetailsModel>($"api/seeding/treatment/{id}");
        }

        public async Task<List<SelectionListModel>> GetTreatmentArticles()
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/assets/treatment");
        }

        public async Task<List<SelectionListModel>> GetTreatmentTypes()
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/assets/treatmentType");
        }

        public async Task<List<GetTreatmentModel>> List(int seedingId)
        {
            return await _httpService
                .GetAsync<List<GetTreatmentModel>>($"api/seeding/{seedingId}/treatment");
        }

        public async Task<bool> Update(ТreatmentDetailsModel editModel)
        {
            return await _httpService
                .PutAsync<bool>("api/seeding/treatment", editModel);
        }
    }
}
