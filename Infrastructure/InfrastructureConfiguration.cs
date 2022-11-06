using Application.Models;
using Application.Models.Common;
using Application.Services;
using Application.Services.Identity;
using Infrastructure.DbContect;
using Infrastructure.Email;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Application.IdentityConstants;
using Serilog;
using Infrastructure.Common.LoggingSettings;
using Serilog.Core;
using Serilog.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Humanizer.Configuration;
using Infrastructure.Common;
using System.Reflection;

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
            services.Configure<ApplicationSettings>(configuration.GetSection(nameof(ApplicationSettings)));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IJwtTokenGenerator, JwtTokenGeneratorService>();
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
                var db = scope.ServiceProvider.GetRequiredService<FarmerDbContext>();
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

        public static void UseSerilogLogging(this IHostBuilder builder)
        {
            builder.UseSerilog((context, services, configuration) =>
            {
                var serilogSettings = new SerilogSettings();
                context.Configuration.Bind(nameof(SerilogSettings), serilogSettings);
                SetUpLogger(configuration, services, serilogSettings);
            });
        }

        private static void SetUpLogger(
            LoggerConfiguration configuration,
            IServiceProvider services,
            SerilogSettings settings)
        {
            if (Enum.TryParse<LogEventLevel>(settings.MinimumLevel.Default, out var defaultLevel))
            {
                configuration.MinimumLevel.ControlledBy(new LoggingLevelSwitch(defaultLevel));
            }
            if (Enum.TryParse<LogEventLevel>(settings.MinimumLevel.Override.Microsoft, out var microsoftMinLevel))
            {
                configuration.MinimumLevel.Override("Microsoft", new LoggingLevelSwitch(microsoftMinLevel));
            }
            if (Enum.TryParse<LogEventLevel>(settings.MinimumLevel.Override.System, out var systemMinLevel))
            {
                configuration.MinimumLevel.Override("System", new LoggingLevelSwitch(systemMinLevel));
            }
            SetUpConsoleLogger(configuration, settings);
        }

        private static void SetUpConsoleLogger(
            LoggerConfiguration loggerConfiguration,
            SerilogSettings serilogSettings)
        {
            if (serilogSettings.Console.IsEnabled)
            {
                Enum.TryParse<LogEventLevel>(serilogSettings.Console.MinLogLevel, out var minLogLevel);
                loggerConfiguration
                    .WriteTo.Console(minLogLevel);
            }
            if (serilogSettings.File.IsEnabled)
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                Enum.TryParse<LogEventLevel>(serilogSettings.File.MinLogLevel, out var fileminLogLevel);
                loggerConfiguration
                    .WriteTo.File($"{path}/{serilogSettings.File.FileName}", fileminLogLevel);
            }
        }
    }
}
