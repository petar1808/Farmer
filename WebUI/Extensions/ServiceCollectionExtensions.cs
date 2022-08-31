using Radzen;

namespace WebUI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRadzenComponents(this IServiceCollection services)
        {
            return services
                .AddScoped<DialogService>()
                .AddScoped<NotificationService>()
                .AddScoped<TooltipService>()
                .AddScoped<ContextMenuService>();
        }
    }
}
