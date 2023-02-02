using Application.Models;
using Application.Services.Identity;
using MediatR;

namespace Application.Features.Identity.Commands.ForgotPassword
{
    public class ForgotUserPasswordCommand : ForgotPasswordInputCommandModel, IRequest<Result>
    {
        public class ForgotUserPasswordCommandHandler : IRequestHandler<ForgotUserPasswordCommand, Result>
        {
            private readonly IIdentityService identityService;

            public ForgotUserPasswordCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result> Handle(
                ForgotUserPasswordCommand request,
                CancellationToken cancellationToken)
                => await identityService.ForgotPassword(request);
        }
    }
}
