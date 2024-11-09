using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class Role : IdentityRole
    {
        private Role() : base()
        {

        }
        public Role(string roleName) : base(roleName)
        {

        }

        public List<User> Users { get; set; } = new List<User>();
    }
}
