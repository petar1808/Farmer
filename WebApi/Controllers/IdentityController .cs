using Application.Models.Users;
using Application.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{

 
    [ApiController]
    [Route("api/identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }


        [HttpPost]
        [Route("createUser")]
        [Authorize(Roles = IdentityRoles.AdminRole)]
        public async Task<ActionResult> CreateUser([FromBody]CreateUserModel createUserModel)
        {
            return await identityService
                .CreateUser(createUserModel)
                .ToActionResult();
        }

        [HttpPost]
        [Route("createUserPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUserPassword([FromBody] CreateUserPasswordModel createUserPasswordModel)
        {
            return await identityService
                .CreateUserPassword(createUserPasswordModel)
                .ToActionResult();
        }

        [HttpPost]
        [Route("changePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            return await identityService
                .ChangePassword(changePasswordModel)
                .ToActionResult();
        }

        [HttpPost]
        [Route("forgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            return await identityService
                .ForgotPassword(forgotPasswordModel)
                .ToActionResult();
        }

        [HttpPost]
        [Route("resetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            return await identityService
                .ResetPassword(resetPasswordModel)
                .ToActionResult();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginInputModel loginInputModel)
        {
            return await identityService
                .Login(loginInputModel)
                .ToActionResult();
        }

        [HttpGet]
        [Route("listUser")]
        [Authorize(Roles = IdentityRoles.AdminRole)]
        public async Task<ActionResult<List<ListUserModel>>> ListUser()
        {
            return await identityService
                .ListUser()
                .ToActionResult();
        }
    }
}
