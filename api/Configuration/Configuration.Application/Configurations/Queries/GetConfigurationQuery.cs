using MediatR;

namespace Configuration.Application.Configurations.Queries
{
    public record GetConfigurationQuery : IRequest<GetConfigurationQueryResult>
    {
        public string Id { get; init; }
    }

    public record GetConfigurationQueryResult
    {
        public string Id { get; init; }
        
        public string Name { get; init; }
        
        public string Type { get; init; }
        
        public string Value { get; init; }
        
        public bool IsActive { get; init; }
        
        public string ApplicationName { get; init; }
    }
}