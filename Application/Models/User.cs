using Microsoft.AspNetCore.Identity;

namespace Application.Models
{
    public class User : IdentityUser
    {
        public User(string userName, string firstName, string lastName)
            : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = userName;
        }

        private User() : base()
        {

        }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public bool Active { get; private set; }

        public List<Role> UserRoles { get; set; } = new List<Role>();

        public User UpdateActive(bool isActive)
        {
            this.Active = isActive;
            return this;
        }
    }
}
