using Application.Mappings;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Tenants
{
    public class ListTenantsWithUsersModel : IMapFrom<User>
    {
        public string Id { get; set; } = default!;

        public int? TanantId { get; set; }

        public string TanantName { get; set; } = default!;

        public string UserEmail { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string UserRole { get; set; } = default!;


        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<User, ListTenantsWithUsersModel>()
               .ForMember(x => x.Id, cfg => cfg.MapFrom(c => c.Id))
               .ForMember(x => x.TanantId, cfg => cfg.MapFrom(c => c.TenantId))
               .ForMember(x => x.TanantName, cfg => cfg.MapFrom(c => c.Tenant!.Name))
               .ForMember(x => x.UserEmail, cfg => cfg.MapFrom(c => c.UserName))
               .ForMember(x => x.UserName, cfg => cfg.MapFrom(c => c.FirstName))
               .ForMember(x => x.UserRole, cfg => cfg.MapFrom(c => c.UserRoles.First().Name));
    }
}
