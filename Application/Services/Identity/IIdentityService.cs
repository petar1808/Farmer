using Application.Features.Identity.Commands;
using Application.Features.Identity.Commands.ChangePassword;
using Application.Features.Identity.Commands.CreateAdmin;
using Application.Features.Identity.Commands.CreatePassword;
using Application.Features.Identity.Commands.CreateUser;
using Application.Features.Identity.Commands.ForgotPassword;
using Application.Features.Identity.Commands.Login;
using Application.Features.Identity.Commands.ResetPassword;
using Application.Features.Identity.Queries.List;
using Application.Models;
using Application.Models.Tenants;
using Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Identity
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
