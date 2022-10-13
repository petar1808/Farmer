using Application.Models.ArableLands;
using Application.Services.ArableLands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/arableLands")]
    [Authorize]
    public class ArableLandController : ControllerBase
    {
        private readonly IArableLandService arableLandService;

        public ArableLandController(IArableLandService arableLandService)
        {
            this.arableLandService = arableLandService;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddArableLandModel arableLandModel)
        {
            return await arableLandService
                .Add(arableLandModel)
                .ToActionResult();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetAreableLandModel>> Get(int id)
        {
            return await arableLandService
                .Get(id)
                .ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAreableLandModel>>> List()
        {
            return await arableLandService
                .List()
                .ToActionResult();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditArableLandModel arableLandModel)
        {
            return await arableLandService
                .Edit(arableLandModel)
                .ToActionResult();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await arableLandService
                .Delete(id)
                .ToActionResult();
        }
    }
}
