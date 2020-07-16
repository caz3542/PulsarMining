using Pulsar.Customers.Api.Data.DbContexts;
using Pulsar.Customers.Api.Infrastructure.ApplicationConfigurationServices;
using Pulsar.Customers.Api.ServiceInstallers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
using Pulsar.Customers.Api.Models.Customers;

namespace Pulsar.Customers.Api.ServiceInstallers.Installers
{
    public class DatabaseRepositoryInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var appConfiguration =
                (ApplicationConfigurationService)serviceProvider.GetService(typeof(ApplicationConfigurationService));

            var logger =
                (ILogger<DatabaseRepositoryInstaller>)serviceProvider.GetRequiredService(
                    typeof(ILogger<DatabaseRepositoryInstaller>));

            if (appConfiguration.UseInMemorySql)
            {
                logger.LogInformation("DatabaseContext: InMemory Database has been configured. Any persistent memory store settings will be ignored.");
                services.AddDbContext<DatabaseContext>(options => { options.UseInMemoryDatabase("InMemory"); });
            }
            else
            {
                if (appConfiguration.IsSqlConfigured)
                {
                    logger.LogInformation("DatabaseContext: SQL Persistence database will be used");
                    services.AddDbContext<DatabaseContext>(options =>
                    {
                        
                        // Connection string suffixes fixed DB Name
                        
                        options.UseSqlServer($"{appConfiguration.SqlConnectionString};Database=PulsarCustomers");
                        
                    });

                    // Connection to SQL server is not tested. Future health check and connection errors should be handled in service.
                }
                else
                {
                    logger.LogCritical("CRITICAL ERROR: Required Database persistence settings not found.");
                    throw new Exception("CRITICAL ERROR: Required Database persistence settings not found.");
                }
            }

            if (appConfiguration.IsDemo)
            {
                try
                {

                    logger.LogInformation("#### !!! Server operating in Demo Mode !!! ####");
                    serviceProvider = services.BuildServiceProvider();
                    var context = (DatabaseContext)serviceProvider.GetService(typeof(DatabaseContext));



                    DemoSeedDatabase(context);
                }
                catch (Exception e)
                {
                    logger.LogCritical($"CRITICAL ERROR: Application is set to Demo Mode but cannot connect to database. {e.Message}");
                }
            }



        }

        /// <summary>
        /// Seeds database with demo data. Should be invoked when IsDemo == True;
        /// </summary>

        private static void DemoSeedDatabase(DatabaseContext context)
        {
            // Seeding function should only run on empty DBs to prevent duplicates.

            context.Database.EnsureCreated();

            if (context.Customers.AsNoTracking().FirstOrDefault() != null) return;
            
            var customerList = new List<Customer>()
            {
                new Customer()
                {
                    Id = Guid.Parse("bd3f5254-6526-4e73-a512-b29066ad4792"),
                    Name = "Alpha Corp",
                    Address = "1 Alpha Drive, Redmond, WA",
                    Zip = "98052",
                    Email = "alpha@clscp.com",
                    IsActive = true
                },
                new Customer()
                {
                    Id = Guid.Parse("ce89c31d-3d30-4981-b7a7-bf97f738206e"),
                    Name = "Bravo Company",
                    Address = "6 Bravo Ct, Washington, DC",
                    Zip = "22202",
                    Email = "bravo@clscp.com",
                    IsActive = true
                },
                new Customer()
                {
                    Id = Guid.Parse("dcdffd17-36f9-4440-b217-1faf2422e71a"),
                    Name = "Charlie Ltd",
                    Address = "1203 Charles Str, New York, New York",
                    Zip = "10036",
                    Email = "carlie@clscp.com",
                    IsActive = true
                }
            };

            context.Customers.AddRange(customerList);
            context.SaveChanges();


        }
    }
}