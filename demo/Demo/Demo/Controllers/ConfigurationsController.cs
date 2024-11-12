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
        public string Get()
        {
            return _configurationReader.GetValue<string>("test2");
        }
    }
}