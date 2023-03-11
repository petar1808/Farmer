using Microsoft.Extensions.Hosting;

namespace Application.Extensions
{
    public static class HostEnvironmentExtensions
    {
        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.EnvironmentName == "Local";
        }
    }
}
