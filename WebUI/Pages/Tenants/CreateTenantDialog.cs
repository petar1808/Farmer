using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Tenants;
using WebUI.ServicesModel.Tenants;

namespace WebUI.Pages.Tenants
{
    public partial class CreateTenantDialog
    {
        [Inject]
        public ITenantService TenantService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public AddTenantModel TenantModel { get; set; } = new AddTenantModel();

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(AddTenantModel tenantModel)
        {
            var addIsSuccess = await TenantService.Add(tenantModel);

            DialogService.Close(addIsSuccess);
        }
    }
}
