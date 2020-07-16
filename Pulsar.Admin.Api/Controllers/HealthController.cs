using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pulsar.Admin.Api.Infrastructure.HealthServices;
using Pulsar.Admin.Api.Models.HealthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Pulsar.Admin.Api.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly HealthService _healthService;

        public HealthController(ILogger<HealthController> logger, HealthService healthService)
        {
            _logger = logger;
            _healthService = healthService;
        }


        [HttpGet("detail")]
        public HealthServiceDetailReadViewModel GetDetailHealth()
        {
            return new HealthServiceDetailReadViewModel()
            {
                Health = _healthService.Health,
                LastHealthMessage = _healthService.LastMessage,
                IsOverridden = _healthService.IsOverriden
            };
        }


        [HttpPost("override/reset")]
        public IActionResult SetOverrideReset(string message = "")
        {
            _healthService.ResetHealth(message);
            return Ok();
        }


        [HttpPost("override/healthy")]
        public IActionResult SetOverrideHealthy(string message = "")
        {
            _healthService.OverrideToHealthy(message);
            return Ok();
        }

        [HttpPost("override/unhealthy")]
        public IActionResult SetOverrideUnhealthy(string message = "")
        {
            _healthService.OverrideToUnhealthy(message);
            return Ok();
        }

        [HttpPost("override/critical")]
        public IActionResult SetOverrideCritical(string message = "")
        {
            _healthService.OverrideToCritical(message);
            return Ok();
        }



        [HttpGet]
        public IActionResult GetHealth()
        {
            if (_healthService.Health == HealthService.HealthStatus.Healthy.ToString())
            {
                return Ok("Healthy");
            }

            if (_healthService.Health == HealthService.HealthStatus.Unhealthy.ToString())
            {
                return NotFound("Unhealthy");
            }

            else return StatusCode(500);
        }

    }
}
