using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pulsar.CoreElements.Api.Data.Services;
using Pulsar.CoreElements.Api.Infrastructure.HealthServices;
using Pulsar.CoreElements.Api.Models.CoreElements;

namespace Pulsar.CoreElements.Api.Controllers
{
    [Route("api/elements")]
    [ApiController]
    public class ElementsController : ControllerBase
    {
        private readonly ILogger<ElementsController> _logger;
        private readonly IPersistentStorageService<CoreElementViewModel, CoreElement> _persistentStorageService;
        private readonly HealthService _healthService;

        public ElementsController(ILogger<ElementsController> logger, IPersistentStorageService<CoreElementViewModel, CoreElement> persistentStorageService, HealthService healthService)
        {
            _logger = logger;
            _persistentStorageService = persistentStorageService;
            _healthService = healthService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllElements()
        {
            if (!_healthService.IsStateHealthy) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                var resultFromStorage = await _persistentStorageService.GetAllAsync();
                return Ok(resultFromStorage);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetElementByName(string name)
        {
            if (!_healthService.IsStateHealthy) return StatusCode(StatusCodes.Status500InternalServerError);

            try
            {
                var resultFromStorage = await _persistentStorageService.GetByExpressionAsync(x => x.Name.Contains(name));
                return Ok(resultFromStorage);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}", Name = "GetElementId")]
        public async Task<IActionResult> GetElementById(string id)
        {
            if (!_healthService.IsStateHealthy) return StatusCode(StatusCodes.Status500InternalServerError);
            if (!Guid.TryParse(id, out var ElementId)) return BadRequest();

            try
            {
                var resultFromStorage = await _persistentStorageService.GetByExpressionAsync(x => x.Id == ElementId);
                if (resultFromStorage.FirstOrDefault() == null) return NotFound();
                return Ok(resultFromStorage.FirstOrDefault());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateElement([FromBody] CoreElementViewModel Element)
        {

            if (!_healthService.IsStateHealthy) return StatusCode(StatusCodes.Status500InternalServerError);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newElement = await _persistentStorageService.CreateAsync(Element);
                return CreatedAtRoute("GetElementId", new CoreElementViewModel { Id = newElement }, newElement.ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
