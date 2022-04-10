using Infrastructure.DbContect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Services;

namespace Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataBase(configuration);
            return services;
        }

        private static IServiceCollection AddDataBase(this IServiceCollection services,
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
    }
}
