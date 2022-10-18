using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
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

        public ForgotPasswordModel ForgotPasswordModel { get; set; } = new ForgotPasswordModel();

        protected async Task OnSubmit(ForgotPasswordModel forgotPasswordModel)
        {
            forgotPasswordModel.ChangePasswordUrl = $"{NavigationManager.BaseUri}/resetPassword";

            await IdentityService.ForgotPassword(forgotPasswordModel);
        }
    }
}
