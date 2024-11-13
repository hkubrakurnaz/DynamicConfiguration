namespace Configuration.Api.Models.Requests
{
    public record PutConfigurationRequest
    {
        public string Name { get; init; }
        
        public string Type { get; init; }
        
        public string Value { get; init; }
        
        public bool IsActive { get; init; }
        
        public string ApplicationName { get; init; }
    }
}