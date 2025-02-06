using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/version")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                          ?? "Unknown";
            return Ok(new { version });
        }
    }
}
