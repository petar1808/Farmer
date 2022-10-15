using Application.Models;
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
        Task<Result> CreateUser(CreateUserModel createUserModel);

        Task<Result> CreateUserPassword(CreateUserPasswordModel createUserPasswordModel);

        Task<Result> ChangePassword(ChangePasswordModel changePasswordModel);

        Task<Result> ForgotPassword(ForgotPasswordModel forgotPasswordModel);

        Task<Result> ResetPassword(ResetPasswordModel resetPasswordModel);

        Task<Result<LoginOutputModel>> Login(LoginInputModel loginInputModel);

        Task<Result<List<ListUserModel>>> ListUser();
    }
}
