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

        public bool Active { get; set; }

        public List<Role> UserRoles { get; set; } = new List<Role>();
    }
}
