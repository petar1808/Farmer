using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Identity;
using WebUI.ServicesModel.Identity;

namespace WebUI.Pages.Identity.ListUsers
{
    public partial class CreateUser
    {
        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public CreateUserModel CreateUserModel { get; set; } = new CreateUserModel();

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(CreateUserModel createUserModel)
        {
            createUserModel.ActivateUserUrl = $"{NavigationManager.BaseUri}createUserPassword";
            var addIsSuccess = await IdentityService.CreateUser(CreateUserModel);

            DialogService.Close(false);
        }
    }
}
