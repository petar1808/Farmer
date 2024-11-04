namespace Application.Models
{
    public class Tenant
    {
        public Tenant(string name)
        {
            Name = name;
        }

        public int Id { get; }

        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
