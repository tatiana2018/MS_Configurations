using Application.ImageApp;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using ms_configuration.Controllers;

namespace TestProject.Controller
{
    public class ImageControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ImagesController _controller;

        public ImageControllerTest()
        {
            // Arrange
            _mediatorMock = new Mock<IMediator>();
            _controller = new ImagesController(_mediatorMock.Object);
        }

        [Fact]
        public async Task PostImage_ReturnsOkResult()
        {
            // Arrange
            var createImageQuery = new CreateImage.Query { /* populate properties as needed */ };
            var expectedResult = new OkObjectResult(new Image());

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateImage.Query>(), default))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.PostImage(createImageQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task PostImage_ReturnsErrorResult_FromMediator()
        {
            // Arrange
            var createImageQuery = new CreateImage.Query { /* populate with valid data */ };
            var errorResult = new BadRequestObjectResult("Validation error");

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateImage.Query>(), default))
                .ReturnsAsync(errorResult);

            // Act
            var result = await _controller.PostImage(createImageQuery);

            // Assert
            Assert.Equal(errorResult, result);
        }


        [Fact]
        public async Task PostImage_ReturnsBadRequest_WithValidationErrors()
        {
            // Act
            var result = await _controller.PostImage(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }
    }
}