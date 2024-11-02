using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
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

        [Inject]
        public NotificationService NotificationService { get; set; } = default!;

        public ChangePasswordModel ChangePasswordModel { get; set; } = new ChangePasswordModel();


        public void OnClose()
        {
            NavigationManager.NavigateTo(NavigationManager.BaseUri);
        }

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
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Info,
                    Detail = "Успешно сменихте вашата парола",
                    Duration = 10000
                });
                NavigationManager.NavigateTo($"{NavigationManager.BaseUri}login");
            }

            NavigationManager.NavigateTo(NavigationManager.BaseUri);
        }
    }
}
