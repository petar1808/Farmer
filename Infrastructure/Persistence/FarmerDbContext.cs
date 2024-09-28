using Application.Models;
using Application.Services;
using Domain.Common;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.DbContect
{
    public class FarmerDbContext : IdentityDbContext<User, Role, string>, IFarmerDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public FarmerDbContext(DbContextOptions options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            Expression<Func<ITenant, bool>> filterExpr = bm => bm.TenantId == _currentUserService.UserTenantId;
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(ITenant)))
                {
                    var parameter = Expression.Parameter(mutableEntityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                    var lambdaExpression = Expression.Lambda(body, parameter);

                    mutableEntityType.SetQueryFilter(lambdaExpression);
                }
            }

            base.OnModelCreating(modelBuilder);
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

        public override async ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
        {
            AssignTenantId(entity); // Assign TenantId for new entities
            return await base.AddAsync(entity, cancellationToken);
        }

        public override EntityEntry<TEntity> Update<TEntity>(TEntity entity)
        {
            //// Assign TenantId if the entity is new or if it doesn't have a TenantId set
            //if (Entry(entity).State == EntityState.Added || (entity is ITenant tenantEntity))
            //{
            //    AssignTenantId(entity);
            //}

            // Check for child entities and assign TenantId for new ones
            foreach (var property in entity.GetType().GetProperties())
            {
                if (typeof(IEnumerable<ITenant>).IsAssignableFrom(property.PropertyType) &&
                    property.GetValue(entity) is IEnumerable<ITenant> childEntities)
                {
                    foreach (var child in childEntities)
                    {
                        var childEntry = Entry(child);
                        // Assign TenantId only if the child is new (not tracked)
                        if (childEntry.State == EntityState.Added)
                        {
                            AssignTenantId(child);
                        }
                    }
                }
            }

            return base.Update(entity);
        }

        private void AssignTenantId(object entity)
        {
            if (entity is ITenant tenantEntity)
            {
                tenantEntity.TenantId = _currentUserService.UserTenantId;
            }

            // Check for child entities and assign TenantId recursively
            foreach (var property in entity.GetType().GetProperties())
            {
                if (property.PropertyType.IsAssignableTo(typeof(ITenant)) && property.GetValue(entity) is ITenant childEntity)
                {
                    AssignTenantId(childEntity);
                }

                // Handle collections of child entities
                if (typeof(IEnumerable<ITenant>).IsAssignableFrom(property.PropertyType) && property.GetValue(entity) is IEnumerable<ITenant> childEntities)
                {
                    foreach (var child in childEntities)
                    {
                        AssignTenantId(child);
                    }
                }
            }
        }
    }
}
