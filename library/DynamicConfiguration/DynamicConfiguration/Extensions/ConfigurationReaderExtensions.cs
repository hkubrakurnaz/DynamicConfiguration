using DynamicConfiguration.Data;
using DynamicConfiguration.Services;
using DynamicConfiguration.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicConfiguration.Extensions
{
    public static class ConfigurationReaderExtensions
    {
        public static IServiceCollection AddConfigurationReader(this IServiceCollection services, DynamicConfigurationSettings settings)
        {
            services.AddSingleton(settings);

            services.AddSingleton<ConfigurationDbContext>();
            
            services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
            
            services.AddSingleton<IConfigurationReader, ConfigurationReader>();
            
            services.AddHostedService<ConfigurationCacheRefresher>();

            return services;
        }
    }
}