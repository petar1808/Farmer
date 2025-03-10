using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApi
{
    public static class HealthCheck
    {
        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(
                    configuration["ConnectionStrings:SqlDefaultConnection"]!, 
                    healthQuery: "select 1", 
                    name: "SQL Server", 
                    failureStatus: HealthStatus.Unhealthy, 
                    tags: new[] { "Database" }
                    );

            var hostName = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");

            var baseUrl = !string.IsNullOrWhiteSpace(hostName)
                ? $"https://{hostName}"
                : "https://localhost:8080";

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(300);   
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);  
                opt.AddHealthCheckEndpoint("Farmer Api", $"{baseUrl}/api/health"); 
            })
            .AddInMemoryStorage();
        }
    }
}
