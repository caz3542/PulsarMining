using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pulsar.Admin.Api.Data.BaseModels;
using Pulsar.Admin.Api.Data.Repositories.Generics;
using Pulsar.Admin.Api.Data.Services;
using Pulsar.Admin.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Admin.Api.ServiceInstallers.Installers
{
    public class PersistentStorageInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Configures services for saving to DB

            // services.AddScoped<IRepositoryBase<BaseDbEntity>, RepositoryBase<BaseDbEntity>();
            // services.AddScoped<IPersistentStorageService<ViewModel, BaseDbEntity>, PersistentStorageService<ViewModel, BaseDbEntity>();
        }
    }
}
