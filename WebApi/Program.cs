using Application;
using Application.Services;
using FluentValidation;
using Infrastructure;
using System.Reflection;
using WebApi.Filters;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_Origins";

builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FilterInvalidModelStateResponse>(int.MinValue);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

if (builder.Configuration.GetValue<string?>("ApplicationInsights:InstrumentationKey") != null)
{
    builder.Services.AddApplicationInsightsTelemetry();
}

builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

builder.Services.AddValidatorsFromAssembly(Assembly.Load(nameof(Application)));

builder.Host.UseSerilogLogging();

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

app.Run();
