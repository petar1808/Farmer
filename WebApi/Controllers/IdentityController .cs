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
        public async Task<ActionResult> CreateUser([FromBody]CreateUserModel createUserModel)
        {
            await identityService.CreateUser(createUserModel);

            return Ok();
        }
    }
}
