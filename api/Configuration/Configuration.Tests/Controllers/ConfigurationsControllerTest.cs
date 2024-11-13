using System.Threading.Tasks;
using AutoFixture;
using Configuration.Api.Controllers;
using Configuration.Api.Models.Requests;
using Configuration.Application.Configurations.Commands;
using Configuration.Application.Configurations.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Configuration.Tests.Controllers
{
    [TestFixture]
    [TestOf(typeof(ConfigurationsController))]
    public class ConfigurationsControllerTest
    {
        private Mock<IMediator> _mediatorMock;
        private ConfigurationsController _controller;

        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ConfigurationsController(_mediatorMock.Object);

            _fixture = new Fixture();
        }

        [Test]
        public async Task Get_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var id = _fixture.Create<string>();
            var queryResult = _fixture.Create<GetConfigurationQueryResult>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetConfigurationQuery>(), default))
                .ReturnsAsync(queryResult);

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult!.Value, Is.EqualTo(queryResult));
        }

        [Test]
        public async Task Get_WithEmptyId_ReturnsBadRequest()
        {
            // Arrange
            var id = string.Empty;

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }

        [Test]
        public async Task GetConfigurations_WithValidRequest_ReturnsOkResult()
        {
            // Arrange
            var request = _fixture.Create<GetConfigurationsRequest>();
            var queryResult = _fixture.Create<GetConfigurationsQueryResult>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetConfigurationsQuery>(), default))
                .ReturnsAsync(queryResult);

            // Act
            var result = await _controller.Get(request);

            // Assert
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(queryResult));
        }

        [Test]
        public async Task Post_WithValidRequest_ReturnsCreatedResult()
        {
            // Arrange
            var request = _fixture.Create<CreateConfigurationRequest>();
            var createdId = _fixture.Create<string>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateConfigurationCommand>(), default))
                .ReturnsAsync(createdId);

            // Act
            var result = await _controller.Post(request);

            // Assert
            Assert.That(result, Is.InstanceOf<ObjectResult>());
            var createdResult = result as ObjectResult;
            Assert.Multiple(() =>
            {
                Assert.That(createdResult?.StatusCode, Is.EqualTo(StatusCodes.Status201Created));
                Assert.That(createdResult?.Value?.GetType().GetProperty("Id")?.GetValue(createdResult.Value, null),
                    Is.EqualTo(createdId));
            });
        }

        [Test]
        public async Task Put_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var id = _fixture.Create<string>();
            var request = _fixture.Create<PutConfigurationRequest>();
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateConfigurationCommand>(), default)).ReturnsAsync(Unit.Value);

            // Act
            var result = await _controller.Put(id, request);

            // Assert
            Assert.That(result, Is.InstanceOf<NoContentResult>());
        }

        [Test]
        public async Task Put_WithEmptyId_ReturnsBadRequest()
        {
            // Arrange
            var id = string.Empty;
            var request = _fixture.Create<PutConfigurationRequest>();
            
            // Act
            var result = await _controller.Put(id, request);

            // Assert
            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
    }
}