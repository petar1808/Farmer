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

            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(300);   
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);  
                opt.AddHealthCheckEndpoint("Farmer Api", "/api/health"); 
            })
            .AddInMemoryStorage();
        }
    }
}
