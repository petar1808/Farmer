using Application.Mappings;
using Application.Models;
using AutoMapper;

namespace Application.Features.Tenants.Queries.ListTenantWithUsers
{
    public class TenantWithUsersOutputQueryModel : IMapFrom<Tenant>
    {
        public int TanantId { get; set; }

        public string TanantName { get; set; } = default!;

        public List<TenantUser> TenantUsers { get; set; } = default!;


        public virtual void Mapping(Profile mapper)
          => mapper.CreateMap<Tenant, TenantWithUsersOutputQueryModel>()
               .ForMember(x => x.TanantId, cfg => cfg.MapFrom(c => c.Id))
               .ForMember(x => x.TanantName, cfg => cfg.MapFrom(c => c.Name))
               .ForMember(x => x.TenantUsers, cfg => cfg.MapFrom(c => c.Users));
    }

    public class TenantUser : IMapFrom<User>
    {
        public string UserEmail { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string UserRole { get; set; } = default!;

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<User, TenantUser>()
            .ForMember(x => x.UserEmail, cfg => cfg.MapFrom(c => c.UserName))
            .ForMember(x => x.UserName, cfg => cfg.MapFrom(c => c.FirstName + " " + c.LastName))
            .ForMember(x => x.UserRole, cfg => cfg.MapFrom(c => c.UserRoles[0].Name));
    }
}
