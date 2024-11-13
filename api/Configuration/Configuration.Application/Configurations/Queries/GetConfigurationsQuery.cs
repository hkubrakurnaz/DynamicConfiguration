using System.Collections.Generic;
using System.Linq;
using Configuration.Domain.Entities;
using MediatR;

namespace Configuration.Application.Configurations.Queries
{
    public record GetConfigurationsQuery : IRequest<GetConfigurationsQueryResult>
    {
        public string Type { get; init; }

        public string ApplicationName { get; init; }
    }

    public record GetConfigurationsQueryResult(List<GetConfigurationsQueryItemResult> Types)
    {
        public static GetConfigurationsQueryResult Map(List<DynamicConfiguration> dynamicConfigurations)
        {
            var items = dynamicConfigurations.Select(x => new GetConfigurationsQueryItemResult(x.Id, x.Name, x.Type, x.Value, x.ApplicationName)).ToList();

            return new GetConfigurationsQueryResult(items);
        }
    }

    public record GetConfigurationsQueryItemResult(
        string Id,
        string Name,
        string Type,
        string Value,
        string ApplicationName);
}