using Application.Models;
using Application.Services;
using Infrastructure.DbContect;
using Infrastructure.Email;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Infrastructure.IdentityConstants;

namespace Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataBase(configuration);
            services.AddIdentity(configuration);
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

        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddEntityFrameworkStores<FarmerDbContext>()
                .AddDefaultTokenProviders();

            var secret = configuration
                .GetSection("ApplicationSettings:Secret").Value;

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    bearer.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Access-Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
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
