using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pulsar.Admin.Api.Infrastructure.HealthServices;
using Pulsar.Admin.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Admin.Api.ServiceInstallers.Installers
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
