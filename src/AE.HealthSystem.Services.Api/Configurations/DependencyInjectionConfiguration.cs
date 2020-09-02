using AE.HealthSystem.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace AE.HealthSystem.Services.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
