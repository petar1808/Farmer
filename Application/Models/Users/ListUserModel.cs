using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class ListUserModel : IMapFrom<User>
    {
        public string UserEmail { get; init; } = default!;

        public string FirstName { get; init; } = default!;

        public string LastName { get; init; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<User, ListUserModel>()
                .ForMember(x => x.UserEmail, cfg => cfg.MapFrom(c => c.UserName));
    }
}
