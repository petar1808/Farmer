using Infrastructure.DbContect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using static Infrastructure.IdentityConstants;
using Infrastructure.Email;
using Application.Models;

namespace Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataBase(configuration);
            services.AddIdentitys();
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }

        private static IServiceCollection AddDataBase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IFarmerDbContext, FarmerDbContext>();
            services.AddDbContext<FarmerDbContext>(opt =>
            {
                var connectionString = configuration
                    .GetSection("ConnectionStrings:DefaultConnection").Value;
                opt.UseSqlServer(connectionString);
            });

            return services;
        }

        private static IServiceCollection AddIdentitys(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                    .AddRoles<Role>()
                    .AddEntityFrameworkStores<FarmerDbContext>();

            services
                .AddHttpContextAccessor()
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.SlidingExpiration = true;
                    options.LoginPath = "/identity/Identity/Login";
                    options.AccessDeniedPath = "/error/forbidden";
                });

            return services;
        }

        public static IApplicationBuilder SeedIdentityUsers(
            this IApplicationBuilder builder,
            IConfiguration configuration)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var roleManager = serviceProvider.GetService<RoleManager<Role>>();
                var userManager = serviceProvider.GetService<UserManager<User>>();

                if (roleManager == null || userManager == null)
                {
                    throw new ApplicationException("Role manager or User Manage not found");
                }

                if (!roleManager.Roles.Any())
                {
                    roleManager.CreateAsync(new Role(IdentityRoles.AdminRole)).GetAwaiter().GetResult();
                    roleManager.CreateAsync(new Role(IdentityRoles.UserRole)).GetAwaiter().GetResult();
                }

                if (!userManager.Users.Any())
                {
                    var user = new User(configuration.GetSection("DefaultUser:Email").Value,"Admin","Admin");

                    user.UpdateActive(true);

                    userManager.CreateAsync(user, configuration.GetSection("DefaultUser:Password").Value).GetAwaiter().GetResult();

                    userManager.AddToRoleAsync(user, IdentityRoles.AdminRole).GetAwaiter().GetResult();
                }

            }
            return builder;
        }
    }
}
