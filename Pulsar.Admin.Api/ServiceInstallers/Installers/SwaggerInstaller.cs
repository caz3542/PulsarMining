using Pulsar.Admin.Api.ServiceInstallers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Pulsar.Admin.Api.ServiceInstallers.Installers
{
    /// <summary>
    ///     Bootstrap swagger
    /// </summary>
    public class SwaggerInstaller : IAppInstaller, IServiceInstaller
    {
        public void InstallApp(IApplicationBuilder app)
        {
            app.UseSwagger();


            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
        }

        public void InstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo { Title = $"{typeof(Program).Assembly.GetName().Name}", Version = "v1" });
            });
        }
    }
}