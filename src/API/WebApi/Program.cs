using Application;
using Application.Services;
using FluentValidation;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Reflection;
using WebApi;
using WebApi.Filters;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File("logs/startup-log.txt", rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();  


Log.Information("Starting application");

var MyAllowSpecificOrigins = "_Origins";

builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FilterInvalidModelStateResponse>(int.MinValue);
});

//Congiguring Health Ckeck
builder.Services.ConfigureHealthChecks(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.ReadFrom.Services(services);
});

//if (builder.Configuration.GetValue<string?>("ApplicationInsights:InstrumentationKey") != null)
//{
//    builder.Services.AddApplicationInsightsTelemetry();
//}

builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddValidatorsFromAssembly(Assembly.Load(nameof(Application)));


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection()
    .UseRouting()
    .SeedIdentityUsers(builder.Configuration)
    .UseCors(MyAllowSpecificOrigins)
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware<ValidationExceptionHandlerMiddleware>()
    .UseEndpoints(endpoints => endpoints.MapControllers());


//HealthCheck Middleware
app.MapHealthChecks("/api/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(conf => 
{
    conf.UIPath = "/healthcheck-ui";
});

try
{
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}
