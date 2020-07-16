using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Pulsar.Customers.Api.Models.AutoMapperProfiles;
using Pulsar.Customers.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.Customers.Api.ServiceInstallers.Installers
{
    public class AutoMapperInstaller : IServiceInstaller
    {
        public void InstallService(IServiceCollection services,
            IConfiguration configuration)
        {
            // Loads AutoMapper configurations located in AutoMapperMappingProfiles

            var mappingConfig = new MapperConfiguration(options =>
                options
                    .AddExpressionMapping()
                    .AddProfile(new AutoMapperMappingProfile())
            );

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}