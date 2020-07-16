using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Pulsar.CoreElements.Api.Infrastructure.ApplicationConfigurationServices;
using Pulsar.CoreElements.Api.Infrastructure.HealthServices;
using Microsoft.Extensions.Logging;

namespace Pulsar.CoreElements.Api.Infrastructure.FluxTimeServerClientServices
{
    public class FluxTimeServerClientService
    {
        private readonly ApplicationConfigurationService _applicationConfigurationService;
        private readonly HealthService _healthService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<FluxTimeServerClientService> _logger;

        public FluxTimeServerClientService(ILogger<FluxTimeServerClientService> logger,
            IHttpClientFactory httpClientFactory, ApplicationConfigurationService applicationConfigurationService, HealthService healthService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _applicationConfigurationService = applicationConfigurationService;
            _healthService = healthService;


        }

        public async Task<DateTime> GetDateTimeUtcNowAsync()
        {
            // Return local date time if service is not configured in AppSettings
            if (_applicationConfigurationService.IsTimeServerConfigured == false)
            {
                _logger.LogDebug("FluxTimeServerClient: Flux Server not configured. Returning local time");
                return DateTime.UtcNow;
            }

            // Must be updated if API is different
            var request = new HttpRequestMessage(HttpMethod.Get, "api/time");

            // Create named client. Typically set via installer.
            var fluxHttpClient = _httpClientFactory.CreateClient("FluxHttpClient");

            try
            {
                if (!_applicationConfigurationService.IsTimeServerConfigured) return DateTime.UtcNow;

                var response = await fluxHttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    var p = await JsonSerializer.DeserializeAsync<string>(responseStream);

                    return DateTime.Parse(p);
                }

                _logger.LogCritical(
                    $"Time server {fluxHttpClient.BaseAddress} configured but error response received.");
            }
            catch (Exception)
            {
                // Log critical errors and set health state to critical. 

                _healthService.SetCritical($"Unable to communicated with configured time server {fluxHttpClient.BaseAddress}");
                //_logger.LogCritical($"Unable to communicated with configured time server {fluxHttpClient.BaseAddress}");
                throw;
            }

            // Fail back to UtcNow; Should not be reached.
            return DateTime.UtcNow;
        }
    }
}