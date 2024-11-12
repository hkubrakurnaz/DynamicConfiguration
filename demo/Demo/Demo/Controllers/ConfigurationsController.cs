using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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