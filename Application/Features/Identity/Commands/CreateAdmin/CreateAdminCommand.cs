using Application.Models;
using Application.Services.Identity;
using MediatR;

namespace Application.Features.Identity.Commands.CreateAdmin
{
    public class CreateAdminCommand : CommonIdentityInputComandModel, IRequest<Result>
    {
        public int TenantId { get; set; }

        public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Result>
        {
            private readonly IIdentityService identityService;

            public CreateAdminCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result> Handle(
                CreateAdminCommand request,
                CancellationToken cancellationToken)
                => await identityService.CreateAdmin(request);
        }
    }
}
