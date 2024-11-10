using WebUI.ServicesModel.Identity;

namespace WebUI.Services.Identity
{
    public interface IIdentityService
    {
        Task<List<ListUserModel>> ListUser();

        Task<string?> Login(string email, string password);

        Task<bool> CreateUser(CreateUserModel createUserModel);

        Task<bool> CreateUserPassword(CreateUserPasswordModel createUserPasswordModel);

        Task<bool> ForgotPassword(ForgotPasswordModel forgotPasswordModel);

        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel);

        Task<bool> ChangePassword(ChangePasswordModel changePasswordModel);
    }
}
