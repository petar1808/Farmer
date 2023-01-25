using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Tenants;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Tenants;

namespace WebUI.Pages.Tenants
{
    public partial class CreateAdminDialog
    {

        [Inject]
        public ITenantService TenantService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public List<SelectionListModel> ListTenants { get; set; } = new List<SelectionListModel>();

        public CreateAdminModel AdminModel { get; set; } = new CreateAdminModel();

        protected async override Task OnInitializedAsync()
        {
            ListTenants = await TenantService.ListSelectionTenants();
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(CreateAdminModel createAdmin)
        {
            createAdmin.ActivateUserUrl = $"{NavigationManager.BaseUri}createUserPassword";
            var addIsSuccess = await TenantService.CreateAdmin(createAdmin);

            DialogService.Close(addIsSuccess);
        }
    }
}
