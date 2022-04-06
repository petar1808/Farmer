using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.DbContect
{
    public class FarmerDbContext : DbContext
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

        public DbSet<ArableLand> ArableLands { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<PerformedWork> PerformedWorks { get; set; }

        public DbSet<Seeding> Seedings { get; set; }

        public DbSet<WorkingSeason> WorkingSeasons { get; set; }
    }
}
