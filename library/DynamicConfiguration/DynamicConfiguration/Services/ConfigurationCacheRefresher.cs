using System;
using System.Threading;
using System.Threading.Tasks;
using DynamicConfiguration.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DynamicConfiguration.Services
{
    public class ConfigurationCacheRefresher : BackgroundService
    {
        private readonly IConfigurationReader _configurationReader;
        private readonly ILogger<ConfigurationCacheRefresher> _logger;
        private readonly DynamicConfigurationSettings _dynamicConfigurationSettings;

        public ConfigurationCacheRefresher(IConfigurationReader configurationReader, 
            ILogger<ConfigurationCacheRefresher> logger, 
            DynamicConfigurationSettings dynamicConfigurationSettings)
        {
            _configurationReader = configurationReader;
            _logger = logger;
            _dynamicConfigurationSettings = dynamicConfigurationSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _configurationReader.RefreshCache();
                    _logger.LogDebug("Configuration cache refreshed successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error occurred while refreshing configuration cache.");
                }

                await Task.Delay(TimeSpan.FromMilliseconds(_dynamicConfigurationSettings.RefreshIntervalInMs), stoppingToken);
            }
        }
    }
}