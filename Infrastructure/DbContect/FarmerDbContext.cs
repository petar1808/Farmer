using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContect
{
    public class FarmerDbContext : DbContext
    {
        public FarmerDbContext()
            : base()
        {
        }
    }
}
