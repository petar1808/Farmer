using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Extensions;
using WebUI.Services.Tenants;
using WebUI.ServicesModel.Tenants;

namespace WebUI.Pages.Tenants
{
    public partial class ListTenantWithUserPage
    {
        [Inject]
        public ITenantService TenantService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public List<ListTenantsWithUsersModel> TenantsWithUsers { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            TenantsWithUsers = await TenantService.ListTenantsWithUsers();
        }

        public async Task CreateTenant()
        {
            var dialogResult = await DialogService.OpenAsync<CreateTenantDialog>($"Добавяне на Ораганизация",
              options: DialogOptionsHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                TenantsWithUsers = await TenantService.ListTenantsWithUsers();
            }
        }

        public async Task CreateAdmin()
        {
            var dialogResult = await DialogService.OpenAsync<CreateAdminDialog>($"Добавяне на Админ",
              options: DialogOptionsHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                TenantsWithUsers = await TenantService.ListTenantsWithUsers();
            }
        }
    }
}
