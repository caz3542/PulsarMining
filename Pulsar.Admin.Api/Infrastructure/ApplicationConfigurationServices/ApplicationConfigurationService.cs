using System;
using System.Linq;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Pulsar.Admin.Api.Infrastructure.ApplicationConfigurationServices
{
    /// <summary>
    ///     Basic service that reads configuration environment to track config across application.
    ///     Expand as required.
    /// </summary>
    public class ApplicationConfigurationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ApplicationConfigurationService> _logger;

        // Configuration Items
        public string SqlConnectionString { get; }
        public string TimeServerHostName { get; }
        public bool DisableFlexTimeServer { get; private set; }
        public bool IsSqlConfigured => !string.IsNullOrEmpty(SqlConnectionString);
        public bool IsTimeServerConfigured
        {
            get
            {
                if (string.IsNullOrEmpty(TimeServerHostName)) return false;
                if (DisableFlexTimeServer) return false;
                return true;
            }
        }

        public bool IsDemo { get; }
        public bool UseInMemorySql { get; }

        public ApplicationConfigurationService(ILogger<ApplicationConfigurationService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            try
            {
                SqlConnectionString = _configuration.GetValue<string>("ApplicationSQL:ConnectionString");
                UseInMemorySql = _configuration.GetValue<bool>("ApplicationSql:UseInMemorySql");

                // Used for auto seeding etc.
                IsDemo = _configuration.GetValue<bool>("IsDemo");

                TimeServerHostName = _configuration.GetValue<string>("FluxTimeServer:Hostname");
                DisableFlexTimeServer = _configuration.GetValue<bool>("FluxTimeServer:Disabled");

            }
            catch (Exception e)
            {
                _logger.LogCritical(
                    $"AppConfigServices: Error parsing AppSettings file. Cannot continue. ERROR: {e.Message}");
                throw;
            }
        }


    }
}