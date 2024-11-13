namespace Configuration.Api.Models.Requests
{
    public class CreateConfigurationRequest
    {
        public string Name { get; init; }
        
        public string Type { get; init; }
        
        public string Value { get; init; }
        
        public string ApplicationName { get; init; }
    }
}