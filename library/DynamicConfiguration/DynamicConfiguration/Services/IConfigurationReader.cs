using System.Threading.Tasks;

namespace DynamicConfiguration.Services
{
    public interface IConfigurationReader
    {
        T GetValue<T>(string key);
        
        Task RefreshCache();
    }
}