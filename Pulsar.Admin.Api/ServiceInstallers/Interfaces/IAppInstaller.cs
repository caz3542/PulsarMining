using Microsoft.AspNetCore.Builder;

namespace Pulsar.Admin.Api.ServiceInstallers.Interfaces
{
    /// <summary>
    ///     Interface for automatic service installation at startup. Inherited classes will be installed via
    ///     UseConfiguredServices under Configure(startup.cs)
    /// </summary>
    public interface IAppInstaller
    {
        void InstallApp(IApplicationBuilder app);
    }
}