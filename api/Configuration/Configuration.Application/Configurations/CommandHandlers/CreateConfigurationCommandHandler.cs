using System.Threading;
using System.Threading.Tasks;
using Configuration.Application.Configurations.Commands;
using Configuration.Domain.Db;
using Configuration.Domain.Entities;
using Configuration.Domain.Exceptions;
using MediatR;

namespace Configuration.Application.Configurations.CommandHandlers
{
    public class CreateConfigurationCommandHandler : IRequestHandler<CreateConfigurationCommand, string>
    {
        private readonly IConfigurationRepository _configurationRepository;

        public CreateConfigurationCommandHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<string> Handle(CreateConfigurationCommand request, CancellationToken cancellationToken)
        {
            await CheckConfigurationExist(request);

            var dynamicConfiguration = new DynamicConfiguration()
            {
                ApplicationName = request.ApplicationName,
                IsActive = true,
                Name = request.Name,
                Type = request.Type,
                Value = request.Value
            };

            var id = await _configurationRepository.CreateConfiguration(dynamicConfiguration);
            return id;
        }

        private async Task CheckConfigurationExist(CreateConfigurationCommand request)
        {
            var dynamicConfigurations = await _configurationRepository.GetConfigurations(request.ApplicationName, request.Name);
            if (dynamicConfigurations.Count != 0)
            {
                throw new ConflictException("Configuration already exists");
            }
        }
    }
}