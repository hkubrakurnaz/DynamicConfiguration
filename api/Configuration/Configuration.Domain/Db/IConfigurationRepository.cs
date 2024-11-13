using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration.Domain.Entities;

namespace Configuration.Domain.Db
{
    public interface IConfigurationRepository
    {
        Task<List<DynamicConfiguration>> GetConfigurations(string applicationName, string type);
        
        Task<DynamicConfiguration> GetConfiguration(string id);
        
        Task<string> CreateConfiguration(DynamicConfiguration config);
        
        Task UpdateConfiguration(string id, DynamicConfiguration config);
        
    }
}