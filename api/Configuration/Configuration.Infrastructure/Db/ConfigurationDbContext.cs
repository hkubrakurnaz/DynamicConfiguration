using System;
using Configuration.Domain.Constants;
using Configuration.Domain.Entities;
using Configuration.Infrastructure.Db.Mappings;
using MongoDB.Driver;

namespace Configuration.Infrastructure.Db
{
    public class ConfigurationDbContext
    {
        private readonly IMongoDatabase _database;

        public ConfigurationDbContext(string connectionString)
        {
            ConfigurationMapping.RegisterMapping();
            
            var mongoUrl = new MongoUrl(connectionString);
            var client = new MongoClient(mongoUrl);
            
            if (client == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }
            
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }
        
        public IMongoCollection<DynamicConfiguration> Configurations => _database.GetCollection<DynamicConfiguration>(CollectionNames.Configurations);
    }
}