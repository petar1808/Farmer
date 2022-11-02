﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using WebUI.Services.Identity;
using WebUI.ServicesModel.Identity;

namespace WebUI.Pages.Identity.User
{
    public partial class CreateUserPassword
    {
        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        public CreateUserPasswordModel CreateUserPasswordModel { get; set; } = new CreateUserPasswordModel();

        protected async Task OnSubmit(CreateUserPasswordModel createUserPasswordModel)
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var queryStrings = QueryHelpers.ParseQuery(uri.Query);

            if (queryStrings.TryGetValue("email", out var email))
            {
                CreateUserPasswordModel.Email = email;
            }

            if (queryStrings.TryGetValue("token", out var token))
            {
                CreateUserPasswordModel.Token = token;
            }

            await IdentityService.CreateUserPassword(CreateUserPasswordModel);

            NavManager.NavigateTo($"{NavManager.BaseUri}/login");
        }
    }
}
