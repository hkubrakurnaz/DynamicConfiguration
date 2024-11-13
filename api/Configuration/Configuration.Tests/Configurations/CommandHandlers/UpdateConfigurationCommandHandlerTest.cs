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
    [TestOf(typeof(UpdateConfigurationCommandHandler))]
    public class UpdateConfigurationCommandHandlerTest
    {
        private Mock<IConfigurationRepository> _configurationRepositoryMock;
        private UpdateConfigurationCommandHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _configurationRepositoryMock = new Mock<IConfigurationRepository>();
            _handler = new UpdateConfigurationCommandHandler(_configurationRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_WithValidRequest_UpdatesConfiguration()
        {
            // Arrange
            var command = _fixture.Create<UpdateConfigurationCommand>();
            var existingConfiguration = _fixture.Create<DynamicConfiguration>();
            _configurationRepositoryMock.Setup(repo => repo.GetConfiguration(command.Id))
                .ReturnsAsync(existingConfiguration);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _configurationRepositoryMock.Verify(repo => repo.UpdateConfiguration(command.Id, existingConfiguration),
                Times.Once);
            Assert.Multiple(() =>
            {
                Assert.That(existingConfiguration.Value, Is.EqualTo(command.Value));
                Assert.That(existingConfiguration.IsActive, Is.EqualTo(command.IsActive));
                Assert.That(existingConfiguration.Type, Is.EqualTo(command.Type));
                Assert.That(existingConfiguration.Name, Is.EqualTo(command.Name));
                Assert.That(existingConfiguration.ApplicationName, Is.EqualTo(command.ApplicationName));
            });
        }

        [Test]
        public void Handle_WhenConfigurationDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var command = _fixture.Create<UpdateConfigurationCommand>();
            _configurationRepositoryMock.Setup(repo => repo.GetConfiguration(command.Id))
                .ReturnsAsync((DynamicConfiguration)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}