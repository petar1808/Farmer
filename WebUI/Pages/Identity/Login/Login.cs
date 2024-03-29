﻿using Blazored.LocalStorage;
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

        public void OnResetPassword()
        {
            NavigationManager.NavigateTo($"{NavigationManager.BaseUri}forgotPassword");
        }
    }
}
