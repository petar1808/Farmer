using Application.Models;
using Application.Services;
using MediatR;

namespace Application.Features.Identity.Commands.ChangePassword
{
    public class ChangePasswordCommand : ChangePasswordInputCommandModel, IRequest<Result>
    {
        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
        {
            private readonly IIdentityService identityService;

            public ChangePasswordCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result> Handle(
                ChangePasswordCommand request,
                CancellationToken cancellationToken)
                => await identityService.ChangePassword(request);

        }
    }
}
