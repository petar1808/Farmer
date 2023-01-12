using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class User : IdentityUser
    {
        public User(string userName, string firstName, string lastName, int? tenantId)
            : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = userName;
            TenantId = tenantId;
        }

        private User() : base()
        {

        }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public int? TenantId { get; set; }

        public Tenant? Tenant { get; set; }

        public bool Active { get; private set; }

        public List<Role> UserRoles { get; set; } = new List<Role>();

        public User UpdateActive(bool isActive)
        {
            this.Active = isActive;
            return this;
        }
    }
}
