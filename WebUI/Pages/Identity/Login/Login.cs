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

        protected bool infoVisible;

        public async Task OnLogin(LoginArgs args)
        {
            var token = await this.IdentityService.Login(args.Username, args.Password);

            if (token != null)
            {
                await LocalStorage.SetItemAsStringAsync("token", token);
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                NavigationManager.NavigateTo(NavigationManager.BaseUri);
            }
        }

        //protected override Task OnInitializedAsync()
        //{
        //    infoVisible = !string.IsNullOrEmpty(info);
        //}

        public async Task OnResetPassword()
        {
            var result = await DialogService.OpenAsync<ForgotPasswordPage>("Reset password");

            //if (result == true)
            //{
            //    infoVisible = true;

            //    info = "Password reset successfully. Please check your email for further instructions.";
            //}
            //NavigationManager.NavigateTo($"{NavigationManager.BaseUri}forgotPassword");
        }
    }
}
