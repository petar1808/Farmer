using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static async Task<ActionResult> ToActionResult(this Task<Result> resultTask)
        {
            var result = await resultTask;

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new { errors = result.Errors });
            }
            

            return new OkObjectResult(result.Succeeded);
        }

        public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<Result<TData>> resultTask)
        {
            var result = await resultTask;

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new { errors = result.Errors});
            }

            return result.Data;
        }
    }
}
