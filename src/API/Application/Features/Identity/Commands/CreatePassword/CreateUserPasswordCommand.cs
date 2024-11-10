using Application.Models;
using Application.Services;
using MediatR;

namespace Application.Features.Identity.Commands.CreatePassword
{
    public class CreateUserPasswordCommand : CreatePasswordInputCommandModel, IRequest<Result>
    {
        public class CreateUserPasswordHandler : IRequestHandler<CreateUserPasswordCommand, Result>
        {
            private readonly IIdentityService identityService;

            public CreateUserPasswordHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result> Handle(
                CreateUserPasswordCommand request,
                CancellationToken cancellationToken)
                => await identityService.CreateUserPassword(request);
        }
    }
}
