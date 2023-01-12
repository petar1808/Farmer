using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Tenants;

namespace WebUI.Services.Tenants
{
    public interface ITenantService
    {
        Task<bool> Add(AddTenantModel tenant);

        Task<bool> CreateAdmin(CreateAdminModel createAdmin);

        Task<List<ListTenantsWithUsersModel>> ListTenantsWithUsers();

        Task<List<SelectionListModel>> ListSelectionTenants();
    }
}
