using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using WebUI.Services.Identity;
using WebUI.ServicesModel.Identity;

namespace WebUI.Pages.Identity.User
{
    public partial class ChangePasswordPage
    {
        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public ChangePasswordModel ChangePasswordModel { get; set; } = new ChangePasswordModel();

        protected async Task OnSubmit(ChangePasswordModel changePasswordModel)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            var email = authState.User.Claims.FirstOrDefault(c => c.Type.ToLower() == "email");
            
            if (email is not null)
            {
                changePasswordModel.Email = email.Value;
            }

            var changePassword = await IdentityService.ChangePassword(changePasswordModel);

            if (changePassword)
            {
                NavigationManager.NavigateTo($"{NavigationManager.BaseUri}/login");
            }

            NavigationManager.NavigateTo(NavigationManager.Uri);
        }
    }
}
