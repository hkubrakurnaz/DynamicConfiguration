using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicConfiguration.Data;
using DynamicConfiguration.Settings;
using Microsoft.Extensions.Logging;

namespace DynamicConfiguration.Services
{
    public class ConfigurationReader : IConfigurationReader
    {
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ILogger<ConfigurationReader> _logger;
        private readonly DynamicConfigurationSettings _options;
        
        private ConcurrentDictionary<string, string> _cache;

        public ConfigurationReader(
            IConfigurationRepository configurationRepository,
            ILogger<ConfigurationReader> logger,
            DynamicConfigurationSettings options)
        {
            _configurationRepository = configurationRepository;
            _logger = logger;
            _options = options;
        }


        public T GetValue<T>(string key)
        {
            if (_cache != null && _cache.TryGetValue(key, out var value))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            throw new KeyNotFoundException($"Key '{key}' not found in the configuration cache.");
        }

        public async Task RefreshCache()
        {
            try
            {
                var configurations = await _configurationRepository.GetConfigurations(_options.ApplicationName);
                var dictionary = configurations.ToDictionary(x => x.Name, x => x.Value);

                _cache = new ConcurrentDictionary<string, string>(dictionary);

                _logger.LogDebug("Configuration cache refreshed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error refreshing configuration cache.");
            }
        }
    }
}