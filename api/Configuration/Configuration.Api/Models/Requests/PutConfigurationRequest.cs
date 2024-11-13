namespace Configuration.Api.Models.Requests
{
    public record PutConfigurationRequest(
        string Name,
        string Type,
        string Value,
        bool IsActive,
        string ApplicationName
    );
}