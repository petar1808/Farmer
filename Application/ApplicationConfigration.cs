using Application.Mappings;
using Application.Services.ArableLands;
using Application.Services.Articles;
using Application.Services.WorikingSeasons;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationConfigration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddAutoMapper();
            services.AddTransient<IArableLandService, ArableLandService>();
            services.AddTransient<IWorkingSeasonService, WorkingSeasonService>();
            services.AddTransient<IArticleService, ArticleService>();

            return services;
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.AddAutoMapper(
                    (_, config) => config
                        .AddProfile(new MappingProfile(assembly)),
                    Array.Empty<Assembly>());
            }

            return services;
        }
    }
}
