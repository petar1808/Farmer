using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator
            => this._mediator ??= this.HttpContext
                ?.RequestServices
                ?.GetService<IMediator>()!;

        protected Task<ActionResult> Send(IRequest<Result> request)
                => this.Mediator.Send(request).ToActionResult();

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request)
                => this.Mediator.Send(request).ToActionResult();
    }
}
