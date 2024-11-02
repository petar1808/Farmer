using Application.Models;
using Application.Services;
using Infrastructure.Common;
using Infrastructure.Common.LoggingSettings;
using Infrastructure.DbContect;
using Infrastructure.Email;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using static Application.IdentityConstants;

namespace Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<InfrastructureSettings>(configuration.GetSection(nameof(InfrastructureSettings)));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGeneratorService>();

            var dbProvider = configuration.GetValue<string>(
                $"{nameof(InfrastructureSettings)}:{nameof(InfrastructureSettings.DatabaseProvider)}");
            var secret = configuration.GetValue<string>(
                $"{nameof(InfrastructureSettings)}:{nameof(InfrastructureSettings.Secret)}");

            var infrastructureSettings = new InfrastructureSettings()
            {
                DatabaseProvider = Enum.Parse<DatabaseProvider>(dbProvider!),
                Secret = secret!
            };

            services.AddDataBase(configuration, infrastructureSettings);
            services.AddIdentity(infrastructureSettings);

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
                        ConfigureSensitiveDataLogging(opt, connectionStrings);
                    });
                    break;

                case DatabaseProvider.SqlServer:
                    services.AddScoped<IFarmerDbContext, SqlFarmerDbContext>();
                    services.AddDbContext<SqlFarmerDbContext>(opt =>
                    {
                        opt.UseSqlServer(connectionStrings.SqlDefaultConnection);
                        ConfigureSensitiveDataLogging(opt, connectionStrings);
                    });
                    break;

                case DatabaseProvider.MySql:
                    services.AddScoped<IFarmerDbContext, MySqlFarmerDbContext>();
                    services.AddDbContext<MySqlFarmerDbContext>(opt =>
                    {
                        var azureMySqlConnectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb");
                        if (!string.IsNullOrWhiteSpace(azureMySqlConnectionString))
                        {
                            string dbhost = Regex.Match(azureMySqlConnectionString, @"Data Source=(.+?);", RegexOptions.None, TimeSpan.FromSeconds(5)).Groups[1].Value;
                            string[] hostParts = dbhost.Split(':');
                            string server = hostParts[0];
                            string port = hostParts.Length > 1 ? hostParts[1] : string.Empty; // Handle cases without port
                            string dbname = Regex.Match(azureMySqlConnectionString, @"Database=(.+?);", RegexOptions.None, TimeSpan.FromSeconds(5)).Groups[1].Value;
                            string dbusername = Regex.Match(azureMySqlConnectionString, @"User Id=(.+?);", RegexOptions.None, TimeSpan.FromSeconds(5)).Groups[1].Value;
                            string dbpassword = Regex.Match(azureMySqlConnectionString, @"Password=(.+?)$", RegexOptions.None, TimeSpan.FromSeconds(5)).Groups[1].Value;

                            connectionStrings.MySqlConnection = $@"server={server};userid={dbusername};password={dbpassword};database={dbname};port={port};pooling=false;convert zero datetime=True;";
                        }

                        opt.UseMySql(
                            connectionStrings.MySqlConnection,
                            ServerVersion.AutoDetect(connectionStrings.MySqlConnection)
                        );

                        ConfigureSensitiveDataLogging(opt, connectionStrings);
                    });
                    break;

                default:
                    throw new NotImplementedException("Unsupported database provider");
            }

            return services;
        }

        private static void ConfigureSensitiveDataLogging(DbContextOptionsBuilder optionsBuilder, ConnectionStrings connectionStrings)
        {
            if (connectionStrings.EnableSensitiveDataLogging)
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
            else if (infrastructureSettings.DatabaseProvider == DatabaseProvider.MySql)
            {
                services
                    .AddIdentity<User, Role>(options =>
                    {
                        options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                        options.SignIn.RequireConfirmedAccount = true;
                    })
                    .AddEntityFrameworkStores<MySqlFarmerDbContext>()
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

        public static IApplicationBuilder SeedIdentityUsers(
            this IApplicationBuilder builder,
            IConfiguration configuration)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var infrastructureSettings = scope.ServiceProvider.GetRequiredService<IOptions<InfrastructureSettings>>();

                FarmerDbContext db;

                if (infrastructureSettings.Value.DatabaseProvider == DatabaseProvider.SqlLite)
                {
                    db = scope.ServiceProvider.GetRequiredService<SqlLiteFarmerDbContext>();
                }
                else if (infrastructureSettings.Value.DatabaseProvider == DatabaseProvider.SqlServer)
                {
                    db = scope.ServiceProvider.GetRequiredService<SqlFarmerDbContext>();
                }
                else if (infrastructureSettings.Value.DatabaseProvider == DatabaseProvider.MySql)
                {
                    db = scope.ServiceProvider.GetRequiredService<MySqlFarmerDbContext>();
                }
                else
                {
                    throw new NotImplementedException("Unsupported database provider");
                }

                db.Database.Migrate();

                var serviceProvider = scope.ServiceProvider;

                var roleManager = serviceProvider.GetService<RoleManager<Role>>();
                var userManager = serviceProvider.GetService<UserManager<User>>();

                if (roleManager == null || userManager == null)
                {
                    throw new ApplicationException("Role manager or User Manage not found");
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
    }
}
