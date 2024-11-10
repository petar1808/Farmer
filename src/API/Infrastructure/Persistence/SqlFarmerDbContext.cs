using Application.Services;
using Infrastructure.DbContect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class SqlFarmerDbContext : FarmerDbContext
    {
        public SqlFarmerDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            ILogger<SqlFarmerDbContext> logger) 
            : base(options, currentUserService, logger)
        {
        }
    }
}
