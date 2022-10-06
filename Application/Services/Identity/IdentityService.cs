using Application.Exceptions;
using Application.Models;
using Application.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Application.Models.IdentityConstants;

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
    }
}
