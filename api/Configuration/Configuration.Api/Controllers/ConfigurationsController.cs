using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using Configuration.Api.Models.Requests;
using Configuration.Application.Configurations.Commands;
using Configuration.Application.Configurations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Configuration.Api.Controllers
{
    [ApiController]
    [Route("v1/configurations")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            
            var getConfigurationQuery = new GetConfigurationQuery
            {
                Id = id
            };

            var getConfigurationQueryResult = await _mediator.Send(getConfigurationQuery);

            return Ok(getConfigurationQueryResult);
        }

        [HttpGet("")]
        public async Task<ActionResult> Get([FromQuery] GetConfigurationsRequest request)
        {
            var getConfigurationsQuery = new GetConfigurationsQuery
            {
                ApplicationName = request.ApplicationName,
                Name = request.Name
            };

            var getConfigurationQueryResult = await _mediator.Send(getConfigurationsQuery);

            return Ok(getConfigurationQueryResult);
        }

        [HttpPost("")]
        public async Task<ActionResult> Post([FromBody] CreateConfigurationRequest request)
        {
            var createConfigurationRequest = new CreateConfigurationCommand()
            {
                ApplicationName = request.ApplicationName,
                Type = request.Type,
                Value = request.Value,
                Name = request.Name
            };

            var id = await _mediator.Send(createConfigurationRequest);

            return StatusCode(StatusCodes.Status201Created, new
            {
                Id = id
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] PutConfigurationRequest request)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            
            var createConfigurationRequest = new UpdateConfigurationCommand()
            {
                Id = id,
                ApplicationName = request.ApplicationName,
                Type = request.Type,
                Value = request.Value,
                Name = request.Name,
                IsActive = request.IsActive
            };

            await _mediator.Send(createConfigurationRequest);

            return NoContent();
        }
    }
}