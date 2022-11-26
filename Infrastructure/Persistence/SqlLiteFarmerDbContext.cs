using Infrastructure.DbContect;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class SqlLiteFarmerDbContext : FarmerDbContext
    {
        public SqlLiteFarmerDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
