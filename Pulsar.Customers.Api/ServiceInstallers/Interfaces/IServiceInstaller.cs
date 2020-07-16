using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Customers.Api.ServiceInstallers.Interfaces
{
    /// <summary>
    ///     Interface for automatic service installation at startup. Inherited classes will be installed via
    ///     AddConfiguredServices under ConfigureServices(startup.cs)
    /// </summary>
    public interface IServiceInstaller
    {
        void InstallService(IServiceCollection services, IConfiguration configuration);
    }
}