using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Tenants;

namespace WebUI.Services.Tenants
{
    public class TenantService : ITenantService
    {
        private readonly IHttpService _httpService;

        public TenantService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<bool> Add(AddTenantModel tenant)
        {
            return await _httpService
                .PostAsync<bool>($"api/tenants", tenant);
        }

        public async Task<bool> CreateAdmin(CreateAdminModel createAdmin)
        {
            return await _httpService
               .PostAsync<bool>($"api/tenants/createAdmin", createAdmin);
        }

        public async Task<List<SelectionListModel>> ListSelectionTenants()
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/tenants/listTenants");
        }

        public async Task<List<ListTenantsWithUsersModel>> ListTenantsWithUsers()
        {
            return await _httpService
                .GetAsync<List<ListTenantsWithUsersModel>>($"api/tenants");
        }
    }
}
