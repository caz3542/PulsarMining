using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pulsar.Customers.Api.Infrastructure.HealthServices;
using Pulsar.Customers.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Customers.Api.ServiceInstallers.Installers
{
    public class HealthServiceInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Init Health Service as singleton

            services.AddSingleton<HealthService>();

            var serviceProvider = services.BuildServiceProvider();
            var applicationConfigurationServiceInstance =
                (HealthService)serviceProvider.GetRequiredService(
                    typeof(HealthService));
        }
    }
}
