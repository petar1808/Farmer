using Application.Mappings;
using Application.Models;
using AutoMapper;

namespace Application.Features.Identity.Queries.List
{
    public class UserListQueryOutputModel : IMapFrom<User>
    {
        public string UserEmail { get; init; } = default!;

        public string FirstName { get; init; } = default!;

        public string LastName { get; init; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<User, UserListQueryOutputModel>()
                .ForMember(x => x.UserEmail, cfg => cfg.MapFrom(c => c.UserName));
    }
}
