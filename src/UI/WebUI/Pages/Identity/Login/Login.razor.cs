using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using WebUI.Pages.Identity.User;
using WebUI.Services.Identity;

namespace WebUI.Pages.Identity.Login
{
    public partial class Login
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; } = default!;

        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        public LoginInputModel LoginInput { get; set; } = new LoginInputModel();

        public bool loading = false;

        public async Task OnLogin(LoginArgs args)
        {
            loading = true;

            var token = await this.IdentityService.Login(args.Username, args.Password);

            if (token != null)
            {
                await LocalStorage.SetItemAsStringAsync("token", token);
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                NavigationManager.NavigateTo(NavigationManager.BaseUri);
            }
            loading = false;
        }

        public async Task OnResetPassword()
        {
            await DialogService.OpenAsync<ForgotPassword>("Забравена парола");
        }
    }
}
