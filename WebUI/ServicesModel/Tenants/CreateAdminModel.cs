namespace WebUI.ServicesModel.Tenants
{
    public class CreateAdminModel
    {
        public string UserEmail { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string ActivateUserUrl { get; set; } = default!;

        public int TenantId { get; set; }
    }
}
