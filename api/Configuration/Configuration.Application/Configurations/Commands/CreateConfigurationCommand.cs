using MediatR;

namespace Configuration.Application.Configurations.Commands
{
    public record CreateConfigurationCommand : IRequest<string>
    {
        public string Name { get; init; }
        
        public string Type { get; init; }
        
        public string Value { get; init; }
        
        public string ApplicationName { get; init; }
    }
}