using Application.Services;
using Infrastructure.DbContect;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class SqlFarmerDbContext : FarmerDbContext
    {
        public SqlFarmerDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService) : base(options, currentUserService)
        {
        }
    }
}
