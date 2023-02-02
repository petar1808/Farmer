using Application.Features.Identity.Commands;
using Application.Features.Identity.Commands.ChangePassword;
using Application.Features.Identity.Commands.CreateAdmin;
using Application.Features.Identity.Commands.CreatePassword;
using Application.Features.Identity.Commands.ForgotPassword;
using Application.Features.Identity.Commands.Login;
using Application.Features.Identity.Commands.ResetPassword;
using Application.Features.Identity.Queries.List;
using Application.Models;

namespace Application.Services
{
    public interface IIdentityService
    {
        Task<Result> CreateUser(CommonIdentityInputComandModel createUserModel);

        Task<Result> CreateUserPassword(CreatePasswordInputCommandModel createUserPasswordModel);

        Task<Result> ChangePassword(ChangePasswordInputCommandModel changePasswordModel);

        Task<Result> ForgotPassword(ForgotPasswordInputCommandModel forgotPasswordModel);

        Task<Result> ResetPassword(ResetPasswordInputCommandModel resetPasswordModel);

        Task<Result<LoginOutputCommandModel>> Login(LoginInputCommandModel loginInputModel);

        Task<Result<List<UserListQueryOutputModel>>> ListUser(UserListQuery userListQuery);

        Task<Result> CreateAdmin(CreateAdminCommand createAdmin);
    }
}
