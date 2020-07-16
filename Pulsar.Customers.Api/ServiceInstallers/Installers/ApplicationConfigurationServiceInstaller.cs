using Pulsar.Customers.Api.Infrastructure.ApplicationConfigurationServices;
using Pulsar.Customers.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Customers.Api.ServiceInstallers.Installers
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