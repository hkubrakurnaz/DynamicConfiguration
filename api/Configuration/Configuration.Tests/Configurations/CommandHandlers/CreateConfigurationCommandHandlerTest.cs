using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Configuration.Application.Configurations.CommandHandlers;
using Configuration.Application.Configurations.Commands;
using Configuration.Domain.Db;
using Configuration.Domain.Entities;
using Configuration.Domain.Exceptions;
using Moq;
using NUnit.Framework;

namespace Configuration.Tests.Configurations.CommandHandlers
{
    [TestFixture]
    [TestOf(typeof(CreateConfigurationCommandHandler))]
    public class CreateConfigurationCommandHandlerTest
    {
        private Mock<IConfigurationRepository> _configurationRepositoryMock;
        private CreateConfigurationCommandHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _configurationRepositoryMock = new Mock<IConfigurationRepository>();
            _handler = new CreateConfigurationCommandHandler(_configurationRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_WithValidRequest_ReturnsConfigurationId()
        {
            // Arrange
            var command = _fixture.Create<CreateConfigurationCommand>();
            var createdId = _fixture.Create<string>();
            _configurationRepositoryMock.Setup(repo => repo.GetConfigurations(command.ApplicationName, command.Type))
                .ReturnsAsync(new List<DynamicConfiguration>());
            _configurationRepositoryMock.Setup(repo => repo.CreateConfiguration(It.IsAny<DynamicConfiguration>()))
                .ReturnsAsync(createdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(createdId));
        }

        [Test]
        public void Handle_WhenConfigurationAlreadyExists_ThrowsConflictException()
        {
            // Arrange
            var command = _fixture.Create<CreateConfigurationCommand>();
            var existingConfigurations = _fixture.CreateMany<DynamicConfiguration>(1).ToList();
            _configurationRepositoryMock.Setup(repo => repo.GetConfigurations(command.ApplicationName, command.Type))
                .ReturnsAsync(existingConfigurations);

            // Act & Assert
            Assert.ThrowsAsync<ConflictException>(async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task Handle_WhenNoExistingConfiguration_CreatesConfiguration()
        {
            // Arrange
            var command = _fixture.Create<CreateConfigurationCommand>();
            _configurationRepositoryMock.Setup(repo => repo.GetConfigurations(command.ApplicationName, command.Type))
                .ReturnsAsync(new List<DynamicConfiguration>());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _configurationRepositoryMock
                .Verify(repo => repo.CreateConfiguration(It.IsAny<DynamicConfiguration>()), Times.Once);
        }
    }
}