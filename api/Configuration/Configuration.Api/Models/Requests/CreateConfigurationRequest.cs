namespace Configuration.Api.Models.Requests
{
    public record CreateConfigurationRequest(string Name, string Type, string Value, string ApplicationName);
}