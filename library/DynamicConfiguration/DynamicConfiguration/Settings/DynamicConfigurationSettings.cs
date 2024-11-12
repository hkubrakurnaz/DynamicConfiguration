namespace DynamicConfiguration.Settings
{
    public class DynamicConfigurationSettings
    {
        public string DatabaseConnectionString { get; set; }

        public string ApplicationName { get; set; }

        public int RefreshIntervalInMs { get; set; } = 60_000;
    }
}