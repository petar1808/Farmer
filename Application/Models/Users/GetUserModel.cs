using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class GetUserModel : IMapFrom<User>
    {
        public string Id { get; set; } = default!;

        public string Email { get; init; } = default!;

        public bool Active { get; init; } = default!;

        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<User, GetUserModel>()
              .ForMember(x => x.Email, cfg => cfg.MapFrom(c => c.UserName))
              .ForMember(x => x.Active, cfg => cfg.MapFrom(c => c.Active));
    }
}
