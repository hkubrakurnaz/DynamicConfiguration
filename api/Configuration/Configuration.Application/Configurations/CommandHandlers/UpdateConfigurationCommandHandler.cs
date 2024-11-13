using System.Threading;
using System.Threading.Tasks;
using Configuration.Application.Configurations.Commands;
using Configuration.Domain.Db;
using Configuration.Domain.Exceptions;
using MediatR;

namespace Configuration.Application.Configurations.CommandHandlers
{
    public class UpdateConfigurationCommandHandler : IRequestHandler<UpdateConfigurationCommand>
    {
        private readonly IConfigurationRepository _configurationRepository;

        public UpdateConfigurationCommandHandler(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }

        public async Task<Unit> Handle(UpdateConfigurationCommand request, CancellationToken cancellationToken)
        {
            var configuration = await _configurationRepository.GetConfiguration(request.Id);

            if (configuration == null)
            {
                throw new NotFoundException("Configuration not found!");
            }
            
            configuration.Value = request.Value;
            configuration.IsActive = request.IsActive;
            configuration.Type = request.Type;
            configuration.Name = request.Name;
            configuration.ApplicationName = request.ApplicationName;

            await _configurationRepository.UpdateConfiguration(request.Id, configuration);

            return Unit.Value;
        }
    }
}