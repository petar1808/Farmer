using Application.Models;
using Application.Services;
using Domain.Common;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;

namespace Infrastructure.DbContect
{
    public class FarmerDbContext : IdentityDbContext<User, Role, string>, IFarmerDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<FarmerDbContext> _logger;
        public FarmerDbContext(
            DbContextOptions options, 
            ICurrentUserService currentUserService, 
            ILogger<FarmerDbContext> logger)
            : base(options)
        {
            _currentUserService = currentUserService;
            _logger = logger;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            Expression<Func<ITenant, bool>> filterExpr = bm => bm.TenantId == _currentUserService.UserTenantId;

            var entityTypes = builder.Model.GetEntityTypes().Where(x => x.ClrType.IsAssignableTo(typeof(ITenant)));

            foreach (var mutableEntityType in entityTypes)
            {
                var parameter = Expression.Parameter(mutableEntityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                mutableEntityType.SetQueryFilter(lambdaExpression);
            }

            base.OnModelCreating(builder);
        }

        public DbSet<ArableLand> ArableLands { get; set; } = default!;

        public DbSet<Article> Articles { get; set; } = default!;

        public DbSet<PerformedWork> PerformedWorks { get; set; } = default!;

        public DbSet<Seeding> Seedings { get; set; } = default!;

        public DbSet<WorkingSeason> WorkingSeasons { get; set; } = default!;

        public override DbSet<User> Users { get; set; } = default!;

        public override DbSet<Role> Roles { get; set; } = default!;

        public DbSet<Treatment> Treatments { get; set; } = default!;

        public DbSet<Subsidy> Subsidies { get; set; } = default!;

        public DbSet<SubsidyByArableLand> SubsidyByArableLands { get; set; } = default!;

        public DbSet<Tenant> Tenants { get; set; } = default!;

        public DbSet<Expense> Expenses { get; set; } = default!;

        public DbSet<ExpenseByArableLand> ExpenseByArableLands { get; set; } = default!;

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            int saveResult;
            var addedEntries = ChangeTracker
                .Entries<IEntity>()
                .Where(x => x.State == EntityState.Added);

            foreach (var entityEntry in addedEntries)
            {
                if (entityEntry.Entity is ITenant tenantEntity)
                {
                    tenantEntity.TenantId = _currentUserService.UserTenantId;
                }
            }


            try
            {
                saveResult = await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "A database concurrency exception occured.");
                throw;
            }

            return saveResult;
        }
    }
}
