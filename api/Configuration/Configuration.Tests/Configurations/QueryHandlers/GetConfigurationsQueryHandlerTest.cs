using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Configuration.Application.Configurations.Queries;
using Configuration.Application.Configurations.QueryHandlers;
using Configuration.Domain.Db;
using Configuration.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Configuration.Tests.Configurations.QueryHandlers
{
    [TestFixture]
    [TestOf(typeof(GetConfigurationsQueryHandler))]
    public class GetConfigurationsQueryHandlerTest
    {
        private Mock<IConfigurationRepository> _configurationRepositoryMock;
        private GetConfigurationsQueryHandler _handler;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _configurationRepositoryMock = new Mock<IConfigurationRepository>();
            _handler = new GetConfigurationsQueryHandler(_configurationRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_WithValidRequest_ReturnsConfigurationsResult()
        {
            // Arrange
            var query = _fixture.Create<GetConfigurationsQuery>();
            var existingConfigurations = _fixture.CreateMany<DynamicConfiguration>().ToList();
            _configurationRepositoryMock.Setup(repo => repo.GetConfigurations(query.ApplicationName, query.Name))
                .ReturnsAsync(existingConfigurations);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Types, Is.Not.Null);
            Assert.That(result.Types, Is.Not.Empty);
            Assert.That(result.Types, Has.Count.EqualTo(existingConfigurations.Count));
        }

        [Test]
        public async Task Handle_WithNoConfigurations_ReturnsEmptyResult()
        {
            // Arrange
            var query = _fixture.Create<GetConfigurationsQuery>();
            _configurationRepositoryMock.Setup(repo => repo.GetConfigurations(query.ApplicationName, query.Name))
                .ReturnsAsync(new List<DynamicConfiguration>());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Types, Is.Empty);
        }
    }
}