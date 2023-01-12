using Domain.Common;

namespace Application.Models
{
    public class Tenant : Entity<int>
    {
        public Tenant(string name)
        {
            Name = name;
        }

        public string Name { get; set; } = default!;

        public List<User> Users { get; set; } = new List<User>();
    }
}
