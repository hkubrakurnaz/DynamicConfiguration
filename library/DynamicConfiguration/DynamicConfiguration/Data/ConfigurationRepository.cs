using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicConfiguration.Data.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace DynamicConfiguration.Data
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IMongoCollection<Configuration> _configurations;

        public ConfigurationRepository(ConfigurationDbContext context)
        {
            _configurations = context.Configurations;
        }

        public async Task<List<Configuration>> GetConfigurations(string applicationName)
        {
            return await _configurations.AsQueryable().Where(x => x.ApplicationName == applicationName).ToListAsync();
        }
    }
}