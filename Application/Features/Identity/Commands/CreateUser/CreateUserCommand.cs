using Application.Models;
using Application.Services.Identity;
using MediatR;

namespace Application.Features.Identity.Commands.CreateUser
{
    public class CreateUserCommand : CommonIdentityInputComandModel, IRequest<Result>
    {
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
        {
            private readonly IIdentityService identityService;

            public CreateUserCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result> Handle(
                CreateUserCommand request,
                CancellationToken cancellationToken)
                => await identityService.CreateUser(request);
        }
    }
}
