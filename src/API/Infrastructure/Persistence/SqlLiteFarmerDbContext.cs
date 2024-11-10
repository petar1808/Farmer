using Application.Services;
using Infrastructure.DbContect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class SqlLiteFarmerDbContext : FarmerDbContext
    {
        public SqlLiteFarmerDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            ILogger<SqlLiteFarmerDbContext> logger) 
            : base(options, currentUserService, logger)
        {
        }
    }
}
