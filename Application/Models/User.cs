using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class User : IdentityUser
    {
        public User(string userName)
            : base(userName)
        {
       
        }

        private User() : base()
        {

        }

        public bool Active { get; private set; }

        public List<Role> UserRoles { get; set; } = new List<Role>();

        public User UpdateActive(bool isActive)
        {
            this.Active = isActive;
            return this;
        }
    }
}
