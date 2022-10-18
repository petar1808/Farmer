using System.Text.Json;
using WebUI.ServicesModel.Identity;

namespace WebUI.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpService httpService;
        public IdentityService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<bool> CreateUser(CreateUserModel createUserModel)
        {
           return await httpService
                .PostAsync<bool>($"api/identity/createUser", createUserModel);
        }

        public async Task<bool> CreateUserPassword(CreateUserPasswordModel createUserPasswordModel)
        {
            return await httpService
                .PostAsync<bool>($"api/identity/createUserPassword", createUserPasswordModel);
        }

        public async Task<bool> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            return await httpService
                .PostAsync<bool>($"api/identity/forgotPassword", forgotPasswordModel);
        }

        public async Task<List<ListUserModel>> ListUser()
        {
            return await httpService
                .GetAsync<List<ListUserModel>>($"api/identity/listUser");
        }

        public async Task<string> Login(string email, string password)
        {
            var model = new LoginInputModel { Email = email, Password = password };

            var result =  await httpService
                .PostAsync<IdentityResult>($"api/identity/login", model);

            return result.Token;
        }

        public async Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return await httpService
                .PostAsync<bool>($"api/identity/resetPassword", resetPasswordModel);
        }
    }

    public class IdentityResult
    {
        public string Token { get; set; }
    }

    public class LoginInputModel
    {
        public string Email { get; set; } = default!;

        public string Password { get; set; } = default!;
    }
}
