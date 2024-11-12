using System.Collections.Generic;
using System.Threading.Tasks;
using DynamicConfiguration.Data.Entities;

namespace DynamicConfiguration.Data
{
    public interface IConfigurationRepository
    {
        Task<List<Configuration>> GetConfigurations(string applicationName);
    }
}