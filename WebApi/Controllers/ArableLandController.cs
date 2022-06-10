using Application.Models.ArableLands;
using Application.Services.ArableLands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/arableLands")]
    public class ArableLandController : ControllerBase
    {
        private readonly IArableLandService arableLandService;

        public ArableLandController( IArableLandService arableLandService)
        {
            this.arableLandService = arableLandService;
        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<GetAreableLandModel>> Get(int id)
        {
            var arableLand = await arableLandService.Get(id);

            return arableLand;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAreableLandModel>>> List()
        {
            var arableLands = await arableLandService.GetAll();

            return arableLands;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AddArableLandModel arableLandModel)
        {
            await arableLandService.Add(arableLandModel.Name, arableLandModel.SizeInDecar);

            return Ok();
        }

       

        [HttpPut]
        public async Task<IActionResult> Edit(EditArableLandModel arableLand)
        {
            await arableLandService.Edit(
                arableLand.Id,
                arableLand.Name,
                arableLand.SizeInDecar);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await arableLandService.Delete(id);

            return Ok();
        }
    }
}
