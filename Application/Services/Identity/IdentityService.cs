using Application.Exceptions;
using Application.Models;
using Application.Models.Users;
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
namespace Application.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        public IdentityService(UserManager<User> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
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

            //user.UpdateLastPasswordChangedDate();

            var addPasswordResult = await this._userManager
                .AddPasswordAsync(
                    user,
                    createUserPasswordModel.Password);

            if (!addPasswordResult.Succeeded)
                throw new BadRequestExeption($"Not successful");
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
                // Added for security reasons so that the user cannot find out if the email exists
                throw new BadRequestExeption($"Not successful");
            }

            var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
            var encoded = HttpUtility.UrlEncode(token);

            await this._emailService.SendResetPasswordEmail(
                user.FirstName + " " + user.LastName,
                user.UserName,
                $"{changePasswordUrl}?email={user.UserName}&token={encoded}");
        }
    }
}
