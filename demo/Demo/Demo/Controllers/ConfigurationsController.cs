using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicConfiguration.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IConfigurationReader _configurationReader;

        public ConfigurationsController(IConfigurationReader configurationReader)
        {
            _configurationReader = configurationReader;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string key = "MaxItemCount", [FromQuery] string type = "integer")
        {
            try
            {
                var result = GetTypes(key, type);
                return Ok(new
                {
                    Result = result,
                });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Post()
        {
            await _configurationReader.RefreshCache();
            return Ok();
        }
        
        private object GetTypes(string key, string type)
        {
            return type.ToLower() switch
            {
                "string" => _configurationReader.GetValue<string>(key),
                "integer" => _configurationReader.GetValue<int>(key),
                "boolean" => _configurationReader.GetValue<bool>(key),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}