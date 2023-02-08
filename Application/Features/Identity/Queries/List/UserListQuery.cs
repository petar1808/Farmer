using Application.Models;
using Application.Services;
using MediatR;

namespace Application.Features.Identity.Queries.List
{
    public class UserListQuery : IRequest<Result<List<UserListQueryOutputModel>>>
    {
        public class UserListQueryHandler : IRequestHandler<UserListQuery, Result<List<UserListQueryOutputModel>>>
        {
            private readonly IIdentityService identityService;

            public UserListQueryHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result<List<UserListQueryOutputModel>>> Handle(
                UserListQuery request,
                CancellationToken cancellationToken)
                => await identityService.ListUser(request);
        }
    }
}
