using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using WebUI.Services.Identity;
using WebUI.ServicesModel.Identity;

namespace WebUI.Pages.Identity.User
{
    public partial class ResetPasswordPage
    {
        bool popup;

        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public ResetPasswordModel ResetPasswordModel { get; set; } = new ResetPasswordModel();

        protected async Task OnSubmit(ResetPasswordModel resetPasswordModel)
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);

            if (queryStrings.TryGetValue("email", out var email))
            {
                resetPasswordModel.Email = email;
            }

            if (queryStrings.TryGetValue("token", out var token))
            {
                resetPasswordModel.Token = token;
            }

            await IdentityService.ResetPassword(resetPasswordModel);

            NavigationManager.NavigateTo($"{NavigationManager.BaseUri}/login");
        }
    }
}
