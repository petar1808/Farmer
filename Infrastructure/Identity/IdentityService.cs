using Application.Exceptions;
using Application.Models;
using Application.Models.Users;
using Application.Services;
using Application.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Application.Models.IdentityConstants;


// Премести го в инфраструтурата, виж как е емаил; В папка Identity
namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public IdentityService(UserManager<User> userManager,
            IEmailService emailService,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task CreateUser(CreateUserModel createUserModel)
        {
            var user = new User(createUserModel.UserEmail, createUserModel.FirstName, createUserModel.LastName);
            var role = IdentityRoles.UserRole;

            var identityResult = await this._userManager.CreateAsync(user);
            if (!identityResult.Succeeded)
            {
                throw new BadRequestExeption($"Not successful");
            }

            var roleResult = await this._userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
                throw new BadRequestExeption($"Not successful");

            var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);

            var encoded = HttpUtility.UrlEncode(token);

            var sendEmailResult = await this._emailService.SendUserCreatedEmail(
                createUserModel.FirstName + " " + createUserModel.LastName,
                createUserModel.UserEmail,
                $"{createUserModel.ActivateUserUrl}?email={createUserModel.UserEmail}&token={token}");

            if (!sendEmailResult)
            {
                throw new BadRequestExeption($"Not successful");
            }
        }

        public async Task CreateUserPassword(CreateUserPasswordModel createUserPasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(createUserPasswordModel.Email);

            if (user == null)
                throw new BadRequestExeption($"Not successful");

            var activateEmailResult = await this._userManager
                .ConfirmEmailAsync(
                    user,
                    createUserPasswordModel.Token);

            if (!activateEmailResult.Succeeded)
                throw new BadRequestExeption($"Not successful");

            var addPasswordResult = await this._userManager
                .AddPasswordAsync(
                    user,
                    createUserPasswordModel.Password);

            if (!addPasswordResult.Succeeded)
                throw new BadRequestExeption($"Not successful");
        }

        public async Task<LoginOutputModel> Login(LoginInputModel loginInputModel)
        {
            var user = await this._userManager
                .Users
                .FirstOrDefaultAsync(x => x.UserName == loginInputModel.Email);

            if (user == null)
                throw new BadRequestExeption($"Not successful");

            var passwordValid = await this._userManager.CheckPasswordAsync(user, loginInputModel.Password);

            if (!passwordValid)
                throw new BadRequestExeption($"Not successful");

            var roles = await this._userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (role == null)
                throw new BadRequestExeption($"Not successful");

            var token = this._jwtTokenGenerator.GenerateToken(user, role);
            return new LoginOutputModel(token, role);
        }

        public async Task ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var user = await this._userManager.FindByNameAsync(changePasswordModel.Email);

            if (user == null)
                throw new BadRequestExeption($"Not successful");

            var changePasswordResult = await this._userManager.ChangePasswordAsync(
                user,
                changePasswordModel.CurrentPassword,
                changePasswordModel.NewPassword);


            if (!changePasswordResult.Succeeded)
            {
                throw new BadRequestExeption($"Not successful");
            }
        }

        public async Task ForgotPassword(string email, string changePasswordUrl)
        {
            var user = await this._userManager
                .Users
                .FirstOrDefaultAsync(x => x.UserName == email);

            if (user == null)
            {
                throw new BadRequestExeption($"Not successful");
            }

            var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
            var encoded = HttpUtility.UrlEncode(token);

            await this._emailService.SendResetPasswordEmail(
                user.FirstName + " " + user.LastName,
                user.UserName,
                $"{changePasswordUrl}?email={user.UserName}&token={encoded}");
        }

        public async Task ResetPassword(string email, string newPassword, string token)
        {
            var user = await this._userManager.FindByNameAsync(email);
            if (user == null)
            {
                throw new BadRequestExeption($"Not successful");
            }

            var resetPasswordResult = await this._userManager.ResetPasswordAsync(user, token, newPassword);
            if (!resetPasswordResult.Succeeded)
            {
                throw new BadRequestExeption($"Not successful");
            }
        }
    }
}
