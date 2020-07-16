using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pulsar.Customers.Api.Data.BaseModels;
using Pulsar.Customers.Api.Data.Repositories.Generics;
using Pulsar.Customers.Api.Data.Services;
using Pulsar.Customers.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pulsar.Customers.Api.Models.Customers;

namespace Pulsar.Customers.Api.ServiceInstallers.Installers
{
    public class PersistentStorageInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            // Configures services for saving to DB

            services.AddTransient<IRepositoryBase<Customer>, RepositoryBase<Customer>>();
            services.AddTransient<IPersistentStorageService<CustomerViewModel, Customer>, PersistentStorageService<CustomerViewModel, Customer>>();
        }
    }
}
