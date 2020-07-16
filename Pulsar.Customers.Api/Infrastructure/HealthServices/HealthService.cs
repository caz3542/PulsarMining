using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Pulsar.Customers.Api.Infrastructure.HealthServices
{
    /// <summary>
    /// Rudimentary health service to show Health Based routing
    /// </summary>

    public class HealthService
    {
        public enum HealthStatus
        {
            Healthy = 1,
            Unhealthy = 2,
            Critical = 3
        }

        private readonly ILogger<HealthService> _logger;
        private int _currentHealth;
        public string Health => ((HealthStatus)_currentHealth).ToString();
        public string LastMessage { get; private set; }
        public bool IsOverriden { get; private set; } = false;

        public bool IsStateHealthy => (HealthStatus)_currentHealth == HealthStatus.Healthy;
        public bool IsStateUnhealthy => (HealthStatus)_currentHealth == HealthStatus.Unhealthy;
        public bool IsStateCritical => (HealthStatus)_currentHealth == HealthStatus.Critical;

        public HealthService(ILogger<HealthService> logger)
        {
            _logger = logger;
            _currentHealth = 1;
            LastMessage = "";
            Task.Run((() => Reset(new CancellationToken())));
        }

        private async Task Reset(CancellationToken cancellationToken)
        {
            await Task.Run(async () =>
                {
                    while (true)
                    {
                        if (!IsOverriden && _currentHealth != (int)HealthStatus.Healthy)
                        {
                            ResetHealth("System reset");
                        }

                        await Task.Delay(30000, cancellationToken);
                    }
                }, cancellationToken);
        }

        public void InvokeOverride()
        {
            IsOverriden = true;
            _logger.LogInformation("HEALTH SERVICE: Health State override enabled");
        }

        public void DisableOverride()
        {
            IsOverriden = false;
            _logger.LogInformation("HEALTH SERVICE: Health State override disabled");
        }

        public void ResetHealth(string logMessage)
        {
            IsOverriden = false;
            _currentHealth = (int)HealthStatus.Healthy;
            LastMessage = $"HEALTH SERVICE: Health State reset: {logMessage}";
            _logger.LogInformation(logMessage);
        }

        public void OverrideToHealthy(string logMessage)
        {
            IsOverriden = true;
            _currentHealth = (int)HealthStatus.Healthy;
            LastMessage = $"Override: {logMessage}";
            _logger.LogInformation($"HEALTH SERVICE: Health State (Override) : Healthy : {logMessage}");
        }

        public void OverrideToUnhealthy(string logMessage)
        {
            IsOverriden = true;
            _currentHealth = (int)HealthStatus.Unhealthy;
            LastMessage = $"Override: {logMessage}";
            _logger.LogWarning($"HEALTH SERVICE: Health State (Override) : UnHealthy : {logMessage}");
        }

        public void OverrideToCritical(string logMessage)
        {
            IsOverriden = true;
            _currentHealth = (int)HealthStatus.Critical;
            LastMessage = $"Override: {logMessage}";
            _logger.LogCritical($"HEALTH SERVICE: Health State (Override) : Critical : {logMessage}");
        }

        public void SetHealthy(string logMessage = "")
        {
            if (IsOverriden) return;

            _currentHealth = (int)HealthStatus.Healthy;
            LastMessage = logMessage;
            _logger.LogInformation($"HEALTH SERVICE: Health State : Healthy : {logMessage}");
        }

        public void SetUnhealthy(string logMessage = "")
        {
            if (IsOverriden) return;
            _currentHealth = (int)HealthStatus.Unhealthy;
            LastMessage = logMessage;
            _logger.LogWarning($"HEALTH SERVICE: Health State: Unhealthy : {logMessage}");
        }

        public void SetCritical(string logMessage = "")
        {
            if (IsOverriden) return;
            _currentHealth = (int)HealthStatus.Critical;
            LastMessage = logMessage;
            _logger.LogWarning($"HEALTH SERVICE: Health State: Critical : {logMessage}");
        }
    }
}