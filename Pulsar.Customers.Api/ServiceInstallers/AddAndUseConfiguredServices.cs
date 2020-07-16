using System;
using System.Linq;
using Pulsar.Customers.Api.ServiceInstallers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Customers.Api.ServiceInstallers
{
    public static class EnableAndUseConfiguredServices
    {
        /// <summary>
        ///     Auto add all services that inherits from IServiceInstaller during startup
        /// </summary>
        public static void AddConfiguredServices(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(IServiceInstaller).Assembly.ExportedTypes.Where(x =>
                    typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IServiceInstaller>().ToList();

            installers.ForEach(installer => installer.InstallService(services, configuration));
        }

        /// <summary>
        ///     Auto add all services that inherits from IAppInstaller during startup
        /// </summary>
        public static void UseConfiguredServices(this IApplicationBuilder app)
        {
            var installers = typeof(IAppInstaller).Assembly.ExportedTypes.Where(x =>
                    typeof(IAppInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IAppInstaller>().ToList();

            installers.ForEach(installer => installer.InstallApp(app));
        }
    }
}