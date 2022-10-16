using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
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
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        public LoginInputModel LoginInput { get; set; } = new LoginInputModel();

        public async Task OnLogin(LoginInputModel loginInput)
        {
            //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImJlcm9pY2l0ZUBhYnYuYmciLCJuYW1lIjoiUGV0YXIgSXZhbm92Iiwicm9sZSI6IlVzZXIiLCJuYmYiOjE2NjU2ODU0MTUsImV4cCI6MTY2NTc3MTgxNCwiaWF0IjoxNjY1Njg1NDE1fQ.v9panCbZj9_BDUCchUXUMPdCadYuFgZFjvTcqjAY3Jg";

            var token = await this.IdentityService.Login(loginInput.Email, loginInput.Password);

            await LocalStorage.SetItemAsync("token", token);
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo(NavigationManager.BaseUri);
        }
    }
}
