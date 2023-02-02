using Application.Models;
using Application.Services.Identity;
using MediatR;

namespace Application.Features.Identity.Commands.Login
{
    public class LoginCommand : LoginInputCommandModel, IRequest<Result<LoginOutputCommandModel>>
    {
        public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginOutputCommandModel>>
        {
            private readonly IIdentityService identityService;

            public LoginCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result<LoginOutputCommandModel>> Handle(
                LoginCommand request,
                CancellationToken cancellationToken)
                => await identityService.Login(request);
        }
    }
}
