using System.Threading;
using System.Threading.Tasks;
using Configuration.Application.Configurations.Queries;
using Configuration.Domain.Db;
using Configuration.Domain.Exceptions;
using MediatR;

namespace Configuration.Application.Configurations.QueryHandlers
{
    public class GetConfigurationQueryHandler : IRequestHandler<GetConfigurationQuery, GetConfigurationQueryResult>
    {
        private readonly IConfigurationRepository _repository;

        public GetConfigurationQueryHandler(IConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetConfigurationQueryResult> Handle(GetConfigurationQuery request, CancellationToken cancellationToken)
        {
            var dynamicConfiguration = await _repository.GetConfiguration(request.Id);

            if (dynamicConfiguration == null)
            {
                throw new NotFoundException("Configuration not found!");
            }
            
            return new GetConfigurationQueryResult()
            {
                Id = dynamicConfiguration.Id,
                ApplicationName = dynamicConfiguration.ApplicationName,
                Name = dynamicConfiguration.Name,
                IsActive = dynamicConfiguration.IsActive,
                Type = dynamicConfiguration.Type,
                Value = dynamicConfiguration.Value
            };
        }
    }
}