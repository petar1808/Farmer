using Application.Features.Identity.Commands.ChangePassword;
using Application.Features.Identity.Commands.CreatePassword;
using Application.Features.Identity.Commands.CreateUser;
using Application.Features.Identity.Commands.ForgotPassword;
using Application.Features.Identity.Commands.Login;
using Application.Features.Identity.Commands.ResetPassword;
using Application.Features.Identity.Queries.List;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.IdentityConstants;

namespace WebApi.Controllers
{


    [ApiController]
    [Route("api/identity")]
    public class IdentityController : BaseApiController
    {
        [HttpPost]
        [Route("createUser")]
        [Authorize(Roles = IdentityRoles.AdminRole)]
        public async Task<ActionResult> CreateUser(
            [FromBody] CreateUserCommand createUserModel)
            => await this.Send(createUserModel);

        [HttpPost]
        [Route("createUserPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUserPassword(
            [FromBody] CreateUserPasswordCommand createUserPasswordModel)
            => await this.Send(createUserPasswordModel);

        [HttpPost]
        [Route("changePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(
            [FromBody] ChangePasswordCommand changePasswordModel)
            => await this.Send(changePasswordModel);

        [HttpPost]
        [Route("forgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(
            [FromBody] ForgotUserPasswordCommand forgotPasswordModel)
            => await this.Send(forgotPasswordModel);

        [HttpPost]
        [Route("resetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(
            [FromBody] ResetPasswordCommand resetPasswordModel)
            => await this.Send(resetPasswordModel);

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginOutputCommandModel>> Login(
            [FromBody] LoginCommand loginInputModel)
            => await this.Send(loginInputModel);

        [HttpGet]
        [Route("listUser")]
        [Authorize(Roles = IdentityRoles.AdminRole)]
        public async Task<ActionResult<List<UserListQueryOutputModel>>> ListUser(
            [FromHeader] UserListQuery userListQuery)
            => await this.Send(userListQuery);
    }
}
