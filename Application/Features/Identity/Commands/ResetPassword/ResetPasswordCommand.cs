using Application.Models;
using Application.Services.Identity;
using MediatR;

namespace Application.Features.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommand : ResetPasswordInputCommandModel, IRequest<Result>
    {
        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
        {
            private readonly IIdentityService identityService;

            public ResetPasswordCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result> Handle(
                ResetPasswordCommand request,
                CancellationToken cancellationToken)
                => await identityService.ResetPassword(request);
        }
    }
}
