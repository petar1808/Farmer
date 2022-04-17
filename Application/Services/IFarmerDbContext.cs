using Application.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Services
{
    public interface IFarmerDbContext
    {
        DbSet<ArableLand> ArableLands { get; } 

        DbSet<Article> Articles { get; } 

        DbSet<PerformedWork> PerformedWorks { get; } 

        DbSet<Seeding> Seedings { get; } 

        DbSet<WorkingSeason> WorkingSeasons { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Role> Roles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

        ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry Remove(object entity);
    }
}
