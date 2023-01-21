﻿using Application.Models;
using Application.Models.Common;
using Application.Services;
using Application.Services.Identity;
using Infrastructure.DbContect;
using Infrastructure.Email;
using Infrastructure.Identity;
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
using System.Reflection;
using Infrastructure.Common;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;

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

            var infrastructureSettings = new InfrastructureSettings();
            configuration.Bind(nameof(InfrastructureSettings), infrastructureSettings);

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

            if (infrastructureSettings.UseSqlLite)
            {
                services.AddScoped<IFarmerDbContext, SqlLiteFarmerDbContext>();

                services.AddDbContext<SqlLiteFarmerDbContext>(opt =>
                {
                    opt.UseSqlite(connectionStrings.SqlLiteConncetion);
                    if (connectionStrings.EnableSensitiveDataLogging)
                    {
                        opt.EnableSensitiveDataLogging()
                            .LogTo(Console.WriteLine);
                    }
                });
            }
            else
            {
                services.AddScoped<IFarmerDbContext, SqlFarmerDbContext>();

                services.AddDbContext<SqlFarmerDbContext>(opt =>
                {
                    opt.UseSqlServer(connectionStrings.SqlDefaultConnection);
                    if (connectionStrings.EnableSensitiveDataLogging)
                    {
                        opt.EnableSensitiveDataLogging()
                            .LogTo(Console.WriteLine);
                    }
                });
            }

            return services;
        }

        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            InfrastructureSettings infrastructureSettings)
        {
            if (infrastructureSettings.UseSqlLite)
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
            else
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
                var infrastructureSettings = scope.ServiceProvider.GetRequiredService<IOptions<InfrastructureSettings>>();

                FarmerDbContext db;

                if (infrastructureSettings.Value.UseSqlLite)
                {
                    db = scope.ServiceProvider.GetRequiredService<SqlLiteFarmerDbContext>();
                }
                else
                {
                    db = scope.ServiceProvider.GetRequiredService<SqlFarmerDbContext>();
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
                    var user = new User(configuration.GetSection("DefaultUser:Email").Value, "System", "Admin", null);

                    user.UpdateActive(true);

                    userManager.CreateAsync(user, configuration.GetSection("DefaultUser:Password").Value).GetAwaiter().GetResult();

                    userManager.AddToRoleAsync(user, IdentityRoles.SystemAdminRole).GetAwaiter().GetResult();
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
