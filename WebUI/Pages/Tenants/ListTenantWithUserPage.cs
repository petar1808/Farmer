using Blazorise;
using Microsoft.AspNetCore.Components;
using Radzen;
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
            await DialogService.OpenAsync<CreateTenantDialog>($"Добавяне на Ораганизация",
              options: new DialogOptions() { Width = "500px", Height = "230px" });

            TenantsWithUsers = await TenantService.ListTenantsWithUsers();
        }

        public async Task CreateAdmin()
        {
            await DialogService.OpenAsync<CreateAdminDialog>($"Добавяне на Админ",
              options: new DialogOptions() { Width = "600px", Height = "400px" });

            TenantsWithUsers = await TenantService.ListTenantsWithUsers();
        }

        public void OnRender(DataGridRenderEventArgs<ListTenantsWithUsersModel> args)
        {
            if (args.FirstRender)
            {
                args.Grid.Groups.Add(new GroupDescriptor() { Property = "TanantName", Title = "Организация" });
                StateHasChanged();
            }
        }
    }
}
