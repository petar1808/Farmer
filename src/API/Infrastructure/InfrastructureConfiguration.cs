using Application.Models;
using Application.Services;
using Infrastructure.Common;
using Infrastructure.DbContect;
using Infrastructure.Email;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using static Application.IdentityConstants;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<InfrastructureSettings>(configuration.GetSection(nameof(InfrastructureSettings)));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IStorageService, BlobStorageService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGeneratorService>();

            var infrastructureSettings = new InfrastructureSettings();
            configuration.Bind(nameof(InfrastructureSettings), infrastructureSettings);

            services.AddDataBase(configuration, infrastructureSettings);
            services.AddIdentity(infrastructureSettings);

            services.AddAzureStorage(configuration);

            return services;
        }

        private static IServiceCollection AddDataBase(
            this IServiceCollection services,
            IConfiguration configuration,
            InfrastructureSettings infrastructureSettings)
        {
            var connectionStrings = new ConnectionStrings();
            configuration.Bind(nameof(ConnectionStrings), connectionStrings);

            // Configure the database context based on the provider
            switch (infrastructureSettings.DatabaseProvider)
            {
                case DatabaseProvider.SqlLite:
                    services.AddScoped<IFarmerDbContext, SqlLiteFarmerDbContext>();
                    services.AddDbContext<SqlLiteFarmerDbContext>(opt =>
                    {
                        opt.UseSqlite(connectionStrings.SqlLiteConnection);
                        ConfigureSensitiveDataLogging(opt, infrastructureSettings.EnableSensitiveDataLogging);
                    });
                    if (infrastructureSettings.BackupEnabled)
                    {
                        services.AddHostedService<SqlLiteBackupHostedService>();
                    }
                    break;

                case DatabaseProvider.SqlServer:
                    services.AddScoped<IFarmerDbContext, SqlFarmerDbContext>();
                    services.AddDbContext<SqlFarmerDbContext>(opt =>
                    {
                        opt.UseSqlServer(connectionStrings.SqlDefaultConnection, sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 3, 
                                maxRetryDelay: TimeSpan.FromSeconds(1), 
                                errorNumbersToAdd: null
                            );
                        });
                        ConfigureSensitiveDataLogging(opt, infrastructureSettings.EnableSensitiveDataLogging);
                    });
                    break;

                default:
                    throw new NotImplementedException("Unsupported database provider");
            }

            return services;
        }

        private static void ConfigureSensitiveDataLogging(
            DbContextOptionsBuilder optionsBuilder, 
            bool enableSensitiveDataLogging)
        {
            if (enableSensitiveDataLogging)
            {
                optionsBuilder.EnableSensitiveDataLogging().LogTo(Console.WriteLine);
            }
        }


        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            InfrastructureSettings infrastructureSettings)
        {
            if (infrastructureSettings.DatabaseProvider == DatabaseProvider.SqlLite)
            {
                services
                    .AddIdentity<User, Role>(options =>
                    {
                        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                        options.SignIn.RequireConfirmedAccount = true;
                    })
                    .AddEntityFrameworkStores<SqlLiteFarmerDbContext>()
                    .AddDefaultTokenProviders();
            }
            else if (infrastructureSettings.DatabaseProvider == DatabaseProvider.SqlServer)
            {
                services
                    .AddIdentity<User, Role>(options =>
                    {
                        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                        options.SignIn.RequireConfirmedAccount = true;
                    })
                    .AddEntityFrameworkStores<SqlFarmerDbContext>()
                    .AddDefaultTokenProviders();
            }
            else
            {
                throw new NotImplementedException("Unsupported database provider");
            }

            var key = Encoding.ASCII.GetBytes(infrastructureSettings.Secret);

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
                                context.Response.Headers.TryAdd("Access-Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static IServiceCollection AddAzureStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var storageSettingsSection = configuration.GetSection(nameof(BlobStorageSettings));
            var storageSettings = storageSettingsSection.Get<BlobStorageSettings>();

            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(storageSettings!.ConnectionString);
            });

            services.Configure<BlobStorageSettings>(storageSettingsSection);

            return services;
        }

        public static IApplicationBuilder SeedIdentityUsers(
            this IApplicationBuilder builder,
            IConfiguration configuration)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var infrastructureSettings = scope.ServiceProvider.GetRequiredService<IOptions<InfrastructureSettings>>();

                FarmerDbContext db = GetDbContext(scope, infrastructureSettings);

                db.Database.Migrate();

                var serviceProvider = scope.ServiceProvider;

                var roleManager = serviceProvider.GetService<RoleManager<Role>>();
                var userManager = serviceProvider.GetService<UserManager<User>>();

                if (roleManager == null)
                {
                    throw new InvalidOperationException("RoleManager is not properly configured in the DI container.");
                }
                if (userManager == null)
                {
                    throw new InvalidOperationException("UserManager is not properly configured in the DI container.");
                }

                if (!roleManager.Roles.Any())
                {
                    roleManager.CreateAsync(new Role(IdentityRoles.SystemAdminRole)).GetAwaiter().GetResult();
                    roleManager.CreateAsync(new Role(IdentityRoles.AdminRole)).GetAwaiter().GetResult();
                    roleManager.CreateAsync(new Role(IdentityRoles.UserRole)).GetAwaiter().GetResult();
                }

                if (!userManager.Users.Any())
                {
                    var user = new User(configuration.GetSection("DefaultUser:Email").Value!, "System", "Admin", null);

                    user.UpdateActive(true);

                    userManager.CreateAsync(user, configuration.GetSection("DefaultUser:Password").Value!).GetAwaiter().GetResult();

                    userManager.AddToRoleAsync(user, IdentityRoles.SystemAdminRole).GetAwaiter().GetResult();
                }

            }
            return builder;
        }

        private static FarmerDbContext GetDbContext(IServiceScope scope, IOptions<InfrastructureSettings> infrastructureSettings)
        {
            FarmerDbContext db;

            if (infrastructureSettings.Value.DatabaseProvider == DatabaseProvider.SqlLite)
            {
                db = scope.ServiceProvider.GetRequiredService<SqlLiteFarmerDbContext>();
            }
            else if (infrastructureSettings.Value.DatabaseProvider == DatabaseProvider.SqlServer)
            {
                db = scope.ServiceProvider.GetRequiredService<SqlFarmerDbContext>();
            }
            else
            {
                throw new NotImplementedException("Unsupported database provider");
            }

            return db;
        }
    }
}
