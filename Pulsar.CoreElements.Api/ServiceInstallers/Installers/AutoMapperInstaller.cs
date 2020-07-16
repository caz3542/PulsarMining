using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Pulsar.CoreElements.Api.Models.AutoMapperProfiles;
using Pulsar.CoreElements.Api.ServiceInstallers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pulsar.CoreElements.Api.ServiceInstallers.Installers
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