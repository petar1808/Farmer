using Application.Models.Users;
using Application.Services.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> CreateUser([FromBody]CreateUserModel createUserModel)
        {
            await identityService.CreateUser(createUserModel);

            return Ok();
        }

        [HttpPost]
        [Route("createUserPassword")]
        public async Task<ActionResult> CreateUserPassword([FromBody] CreateUserPasswordModel createUserPasswordModel)
        {
            await identityService.CreateUserPassword(createUserPasswordModel);

            return Ok();
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            await identityService.ChangePassword(changePasswordModel);

            return Ok();
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<ActionResult> ForgotPassword(string email, string changePasswordUrl)
        {
            await identityService.ForgotPassword(email, changePasswordUrl);

            return Ok();
        }
    }
}
