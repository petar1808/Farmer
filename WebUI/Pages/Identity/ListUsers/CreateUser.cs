using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Identity;
using WebUI.ServicesModel.Identity;

namespace WebUI.Pages.Identity.ListUsers
{
    public partial class CreateUser
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public CreateUserModel CreateUserModel { get; set; } = new CreateUserModel();

        protected async Task OnSubmit(CreateUserModel createUserModel)
        {
            createUserModel.ActivateUserUrl = $"{NavigationManager.BaseUri}/createUserPassword";
            var addIsSuccess = await IdentityService.CreateUser(CreateUserModel);
            if (addIsSuccess)
            {
                StatusClass = "alert-success";
                Message = "New ArableLand added successfully.";
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new ArableLand. Please try again.";
            }
            DialogService.Close(false);
        }
    }
}
