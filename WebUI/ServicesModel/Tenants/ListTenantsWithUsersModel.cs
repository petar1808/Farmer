using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Tenants
{
    public class ListTenantsWithUsersModel
    {
        public int? TanantId { get; set; }

        public string TanantName { get; set; } = default!;

        public List<TenatUser> TenantUsers { get; set; } = default!;
    }

    public class TenatUser
    {
        public string UserEmail { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string UserRole { get; set; } = default!;
    }
}
