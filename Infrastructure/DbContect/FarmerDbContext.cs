using Application.Services;
using Domain.Models;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace Infrastructure.DbContect
{
    public class FarmerDbContext : DbContext, IFarmerDbContext
    {
        public FarmerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ArableLand> ArableLands { get; set; } = default!;

        public DbSet<Article> Articles { get; set; } = default!;

        public DbSet<PerformedWork> PerformedWorks { get; set; } = default!;

        public DbSet<Seeding> Seedings { get; set; } = default!;

        public DbSet<WorkingSeason> WorkingSeasons { get; set; } = default!;
    }
}
