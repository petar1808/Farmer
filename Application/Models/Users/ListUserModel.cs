using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class ListUserModel 
    {
        public string Id { get; init; } = default!;

        public string Email { get; init; } = default!;

        public string Role { get; init; } = default!;

        public bool Active { get; init; } = default!;

        //public virtual void Mapping(Profile mapper)
        //  => mapper.CreateMap<User, ListUserModel>()
        //      .ForMember(x => x.Email, cfg => cfg.MapFrom(c => c.UserName))
        //      .ForMember(x => x.Role, cfg => cfg.MapFrom(c => c.UserRoles.First().Name))
        //      .ForMember(x => x.Active, cfg => cfg.MapFrom(c => c.Active));
    }
}
