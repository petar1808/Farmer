using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Extensions;

namespace WebApi.Filters
{
    public class FilterInvalidModelStateResponse : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (var state in context.ModelState)
                {
                    errors.AddRange(state.Value.Errors.Select(c => c.ErrorMessage));
                }

                context.Result = new BadRequestObjectResult(Result.Failure(errors));

                return;
            }
            await next();
        }
    }
}
