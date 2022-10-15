using Application.Models;
using Application.Models.Users;
using Application.Services;
using Application.Services.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web;
using static Application.Models.IdentityConstants;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper mapper;
        public IdentityService(UserManager<User> userManager,
            IEmailService emailService,
            IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtTokenGenerator = jwtTokenGenerator;
            this.mapper = mapper;
        }

        public async Task<Result> CreateUser(CreateUserModel createUserModel)
        {
            var user = new User(createUserModel.UserEmail, createUserModel.FirstName, createUserModel.LastName);
            var role = IdentityRoles.UserRole;

            var identityResult = await this._userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                if (identityResult.Errors.Any(x => x.Code == "DuplicateUserName"))
                {
                    return "Имейлът вече съществува!";
                }
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
                return "Имейлът не беше изпратен успешно!";
            }

            return Result.Success;
        }

        public async Task<Result> CreateUserPassword(CreateUserPasswordModel createUserPasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(createUserPasswordModel.Email);

            if (user == null)
            {
                return "Невалидни идентификационни данни!";
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
                return "Невалидни идентификационни данни!";
            }

            return Result.Success;
        }

        public async Task<Result<LoginOutputModel>> Login(LoginInputModel loginInputModel)
        {
            var user = await this._userManager
                .Users
                .FirstOrDefaultAsync(x => x.UserName == loginInputModel.Email);

            if (user == null)
            {
                return "Грешна парола или имейл!";
            }

            var passwordValid = await this._userManager.CheckPasswordAsync(user, loginInputModel.Password);

            if (!passwordValid)
            {
                return "Грешна парола или имейл!";
            }

            var roles = await this._userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role == null)
            {
                return "Потребителят не е присвоен на никаква роля";
            }

            var token = this._jwtTokenGenerator.GenerateToken(user, role);
            return new LoginOutputModel(token);
        }

        public async Task<Result> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(changePasswordModel.Email);

            if (user == null)
            {
                return "Невалидни идентификационни данни!";
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

        public async Task<Result> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
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
                user.UserName,
                $"{forgotPasswordModel.ChangePasswordUrl}?email={user.UserName}&token={encoded}");

            return Result.Success;
        }

        public async Task<Result> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(resetPasswordModel.Email);
            if (user == null)
            {
                return Result.Success;
            }

            var resetPasswordResult = await this._userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.NewPassword);
            if (!resetPasswordResult.Succeeded)
            {
                return "Невалидни идентификационни данни!";
            }
            
            return Result.Success;
        }

        public async Task<Result<List<ListUserModel>>> ListUser()
        {
            var users = await _userManager
                .Users
                .Where(x => x.UserRoles.Any(x => x.Name != "Admin"))
                .ToListAsync();

            if (users == null)
            {
                return "";
            }

            var result = mapper.Map<List<ListUserModel>>(users);

            return result;
        }
    }
}
