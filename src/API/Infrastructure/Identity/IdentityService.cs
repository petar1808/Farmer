using Application.Features.Identity.Commands;
using Application.Features.Identity.Commands.ChangePassword;
using Application.Features.Identity.Commands.CreateAdmin;
using Application.Features.Identity.Commands.CreatePassword;
using Application.Features.Identity.Commands.ForgotPassword;
using Application.Features.Identity.Commands.Login;
using Application.Features.Identity.Commands.ResetPassword;
using Application.Features.Identity.Queries.List;
using Application.Models;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;
using static Application.IdentityConstants;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper mapper;
        private readonly ICurrentUserService _currentUserService;

        private const string commonError = "Невалидни идентификационни данни!";
        public IdentityService(UserManager<User> userManager,
            IEmailService emailService,
            IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtTokenGenerator = jwtTokenGenerator;
            this.mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result> CreateUser(CommonIdentityInputComandModel createUserModel)
        {
            var user = new User(createUserModel.UserEmail, createUserModel.FirstName, createUserModel.LastName, _currentUserService.UserTenantId);
            var role = IdentityRoles.UserRole;

            var identityResult = await this._userManager.CreateAsync(user);
            if (!identityResult.Succeeded 
                && identityResult.Errors.Any(x => x.Code == "DuplicateUserName"))
            {
                return "Имейлът вече съществува!";
            }

            var roleResult = await this._userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                return "Грешка при задаването на роля!";
            }

            var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);

            var encoded = HttpUtility.UrlEncode(token);

            var sendEmailResult = await this._emailService.SendUserCreatedEmail(
                createUserModel.FirstName + " " + createUserModel.LastName,
                createUserModel.UserEmail,
                $"{createUserModel.ActivateUserUrl}?email={createUserModel.UserEmail}&token={encoded}");

            if (!sendEmailResult)
            {
                await _userManager.DeleteAsync(user);

                return "Имейлът не беше изпратен успешно!";
            }

            return Result.Success;
        }

        public async Task<Result> CreateAdmin(CreateAdminCommand createAdmin)
        {
            var admin = new User(createAdmin.UserEmail, createAdmin.FirstName, createAdmin.LastName, createAdmin.TenantId);
            var role = IdentityRoles.AdminRole;

            var identityResult = await this._userManager.CreateAsync(admin);
            if (!identityResult.Succeeded 
                && identityResult.Errors.Any(x => x.Code == "DuplicateUserName"))
            {
                return "Имейлът вече съществува!";
            }

            var roleResult = await this._userManager.AddToRoleAsync(admin, role);
            if (!roleResult.Succeeded)
            {
                return "Грешка при задаването на роля!";
            }

            var token = await this._userManager.GenerateEmailConfirmationTokenAsync(admin);

            var encoded = HttpUtility.UrlEncode(token);

            var sendEmailResult = await this._emailService.SendUserCreatedEmail(
                createAdmin.FirstName + " " + createAdmin.LastName,
                createAdmin.UserEmail,
                $"{createAdmin.ActivateUserUrl}?email={createAdmin.UserEmail}&token={encoded}");

            if (!sendEmailResult)
            {
                await _userManager.DeleteAsync(admin);

                return "Имейлът не беше изпратен успешно!";
            }

            return Result.Success;
        }

        public async Task<Result> CreateUserPassword(CreatePasswordInputCommandModel createUserPasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(createUserPasswordModel.Email);

            if (user == null)
            {
                return commonError;
            }

            var activateEmailResult = await this._userManager
                .ConfirmEmailAsync(
                    user,
                    createUserPasswordModel.Token);

            if (!activateEmailResult.Succeeded)
            {
                return "Невалиден или изтекъл токен!";
            }

            var addPasswordResult = await this._userManager
                .AddPasswordAsync(
                    user,
                    createUserPasswordModel.Password);

            if (!addPasswordResult.Succeeded)
            {
                return commonError;
            }

            return Result.Success;
        }

        public async Task<Result<LoginOutputCommandModel>> Login(LoginInputCommandModel loginInputModel)
        {
            var user = await this._userManager
                .Users
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(x => x.UserName == loginInputModel.Email);

            if (user == null)
            {
                return "Грешна парола или имейл!";
            }

            var passwordValid = await this._userManager.CheckPasswordAsync(user, loginInputModel.Password!);

            if (!passwordValid)
            {
                return "Грешна парола или имейл!";
            }

            var roles = await this._userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role == null)
            {
                return "Потребителят не е присвоен към никаква роля";
            }

            var token = this._jwtTokenGenerator.GenerateToken(user, role);
            return new LoginOutputCommandModel(token);
        }

        public async Task<Result> ChangePassword(ChangePasswordInputCommandModel changePasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(changePasswordModel.Email);

            if (user == null)
            {
                return commonError;
            }

            var changePasswordResult = await this._userManager.ChangePasswordAsync(
                user,
                changePasswordModel.CurrentPassword,
                changePasswordModel.NewPassword);


            if (!changePasswordResult.Succeeded)
            {
                return "Невалидна парола";
            }

            return Result.Success;
        }

        public async Task<Result> ForgotPassword(ForgotPasswordInputCommandModel forgotPasswordModel)
        {
            var user = await this._userManager
                .Users
                .FirstOrDefaultAsync(x => x.UserName == forgotPasswordModel.Email);

            if (user == null)
            {
                return Result.Success;
            }

            var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
            var encoded = HttpUtility.UrlEncode(token);

            await this._emailService.SendResetPasswordEmail(
                user.FirstName + " " + user.LastName,
                user.UserName ?? "",
                $"{forgotPasswordModel.ChangePasswordUrl}?email={user.UserName}&token={encoded}");

            return Result.Success;
        }

        public async Task<Result> ResetPassword(ResetPasswordInputCommandModel resetPasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(resetPasswordModel.Email);
            if (user == null)
            {
                return Result.Success;
            }

            var resetPasswordResult = await this._userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
            if (!resetPasswordResult.Succeeded)
            {
                return commonError;
            }

            return Result.Success;
        }

        public async Task<Result<List<UserListQueryOutputModel>>> ListUser(UserListQuery userListQuery)
        {
            var users = _userManager
                .Users
                .Where(x => x.TenantId == _currentUserService.UserTenantId)
                .Where(x => x.UserRoles.Any(x => x.Name != IdentityRoles.AdminRole && x.Name != IdentityRoles.SystemAdminRole))
                .AsQueryable();

            var result = await mapper.ProjectTo<UserListQueryOutputModel>(users).ToListAsync();

            return result;
        }
    }
}
