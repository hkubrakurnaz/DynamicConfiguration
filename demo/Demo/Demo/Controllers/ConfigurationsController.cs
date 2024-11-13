using System.Collections.Generic;
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
        public IActionResult Get([FromQuery] string key)
        {
            try
            {
                var result = _configurationReader.GetValue<object>(key);
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
    }
}