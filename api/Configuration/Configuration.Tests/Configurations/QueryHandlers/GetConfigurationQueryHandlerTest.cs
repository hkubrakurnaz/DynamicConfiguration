using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Configuration.Application.Configurations.Queries;
using Configuration.Application.Configurations.QueryHandlers;
using Configuration.Domain.Db;
using Configuration.Domain.Entities;
using Configuration.Domain.Exceptions;
using Moq;
using NUnit.Framework;

namespace Configuration.Tests.Configurations.QueryHandlers
{
    [TestFixture]
    [TestOf(typeof(GetConfigurationQueryHandler))]
    public class GetConfigurationQueryHandlerTest
    {
        private Mock<IConfigurationRepository> _repositoryMock;
        private GetConfigurationQueryHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IConfigurationRepository>();
            _handler = new GetConfigurationQueryHandler(_repositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_WithValidId_ReturnsConfigurationResult()
        {
            // Arrange
            var query = _fixture.Create<GetConfigurationQuery>();
            var existingConfiguration = _fixture.Create<DynamicConfiguration>();
            _repositoryMock.Setup(repo => repo.GetConfiguration(query.Id))
                .ReturnsAsync(existingConfiguration);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(existingConfiguration.Id));
                Assert.That(result.ApplicationName, Is.EqualTo(existingConfiguration.ApplicationName));
                Assert.That(result.Name, Is.EqualTo(existingConfiguration.Name));
                Assert.That(result.IsActive, Is.EqualTo(existingConfiguration.IsActive));
                Assert.That(result.Type, Is.EqualTo(existingConfiguration.Type));
                Assert.That(result.Value, Is.EqualTo(existingConfiguration.Value));
            });
        }

        [Test]
        public void Handle_WhenConfigurationDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var query = _fixture.Create<GetConfigurationQuery>();
            _repositoryMock.Setup(repo => repo.GetConfiguration(query.Id))
                .ReturnsAsync((DynamicConfiguration)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(query, CancellationToken.None));
        }
    }
}