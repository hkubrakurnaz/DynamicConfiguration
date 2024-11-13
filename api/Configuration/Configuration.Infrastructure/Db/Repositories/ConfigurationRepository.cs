using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration.Domain.Db;
using Configuration.Domain.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Configuration.Infrastructure.Db.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly IMongoCollection<DynamicConfiguration> _configurations;

        public ConfigurationRepository(ConfigurationDbContext context)
        {
            _configurations = context.Configurations;
        }
        
        public async Task<List<DynamicConfiguration>> GetConfigurations(string applicationName, string name)
        {
            var dynamicConfigurations = _configurations.AsQueryable().Where(x => x.IsActive);

            if (!string.IsNullOrEmpty(applicationName))
            {
                dynamicConfigurations = dynamicConfigurations.Where(x => x.ApplicationName == applicationName);
            }

            if (!string.IsNullOrEmpty(name))
            {
                dynamicConfigurations = dynamicConfigurations.Where(x => x.Name == name);
            }

            var configurations = await dynamicConfigurations.ToListAsync();

            return configurations;
        }

        public async Task<DynamicConfiguration> GetConfiguration(string id)
        {
            return await _configurations.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> CreateConfiguration(DynamicConfiguration config)
        {
            await _configurations.InsertOneAsync(config);

            return config.Id;
        }

        public async Task UpdateConfiguration(string id, DynamicConfiguration config)
        {
            await _configurations.ReplaceOneAsync(c => c.Id == id, config);;
        }
    }
}