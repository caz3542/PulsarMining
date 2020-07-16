using Pulsar.Inventory.Api.Infrastructure.ApplicationConfigurationServices;
using Pulsar.Inventory.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Inventory.Api.ServiceInstallers.Installers
{
    public class ApplicationConfigurationServiceInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ApplicationConfigurationService>();

            // Invoke singleton

            var serviceProvider = services.BuildServiceProvider();
            var applicationConfigurationServiceInstance =
                (ApplicationConfigurationService)serviceProvider.GetRequiredService(
                    typeof(ApplicationConfigurationService));
        }
    }
}