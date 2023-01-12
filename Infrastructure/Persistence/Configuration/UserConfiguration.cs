using Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .HasOne(x => x.Tenant)
               .WithMany(x => x.Users)
               .HasForeignKey(p => p.TenantId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.UserRoles)
                .WithMany(x => x.Users)
                .UsingEntity<IdentityUserRole<string>>();
        }
    }
}
