using Application;
using Application.Services;
using Infrastructure;
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

builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();

builder.Host.UseSerilogLogging();

var app = builder.Build();

if (app.Environment.IsDevelopment())
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
