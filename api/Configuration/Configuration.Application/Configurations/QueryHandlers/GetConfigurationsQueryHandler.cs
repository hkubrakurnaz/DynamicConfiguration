using System.Threading;
using System.Threading.Tasks;
using Configuration.Application.Configurations.Queries;
using Configuration.Domain.Db;
using MediatR;

namespace Configuration.Application.Configurations.QueryHandlers
{
    public class GetConfigurationsQueryHandler : IRequestHandler<GetConfigurationsQuery, GetConfigurationsQueryResult>
    {
        private readonly IConfigurationRepository _configurationRepository;

        public GetConfigurationsQueryHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<GetConfigurationsQueryResult> Handle(GetConfigurationsQuery request, CancellationToken cancellationToken)
        {
            var dynamicConfigurations = await _configurationRepository.GetConfigurations(request.ApplicationName, request.Type);

            return GetConfigurationsQueryResult.Map(dynamicConfigurations);
        }
    }
}