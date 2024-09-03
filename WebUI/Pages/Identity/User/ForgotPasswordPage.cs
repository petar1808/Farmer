using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Identity;
using WebUI.ServicesModel.Identity;

namespace WebUI.Pages.Identity.User
{
    public partial class ForgotPasswordPage
    {
        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public NotificationService NotificationService { get; set; } = default!;

        public ForgotPasswordModel ForgotPasswordModel { get; set; } = new ForgotPasswordModel();

        public void OnClose()
        {
            NavigationManager.NavigateTo($"{NavigationManager.BaseUri}/login");
        }

        protected async Task OnSubmit(ForgotPasswordModel forgotPasswordModel)
        {
            forgotPasswordModel.ChangePasswordUrl = $"{NavigationManager.BaseUri}resetPassword";

            var isSuccess = await IdentityService.ForgotPassword(forgotPasswordModel);

            if (isSuccess)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Info,
                    Detail = "Ако вашият имейл съществува в ситемата ще получите имейл с инструкции",
                    Duration = 10000
                });
                NavigationManager.NavigateTo($"{NavigationManager.BaseUri}login");
            }

            NavigationManager.NavigateTo(NavigationManager.Uri);
        }
    }
}
