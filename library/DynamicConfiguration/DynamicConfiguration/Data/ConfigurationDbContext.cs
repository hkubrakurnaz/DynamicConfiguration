using System;
using DynamicConfiguration.Constants;
using DynamicConfiguration.Data.Entities;
using DynamicConfiguration.Settings;
using MongoDB.Driver;

namespace DynamicConfiguration.Data
{
    public class ConfigurationDbContext
    {
        private readonly IMongoDatabase _database;
        
        public ConfigurationDbContext(DynamicConfigurationSettings options)
        {
            var mongoUrl = new MongoUrl(options.DatabaseConnectionString);
            var client = new MongoClient(mongoUrl);
            
            if (client == null)
            {
                throw new ArgumentNullException(nameof(options.DatabaseConnectionString));
            }

            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }
        
        public IMongoCollection<Configuration> Configurations => _database.GetCollection<Configuration>(CollectionNames.Configurations);
    }
}