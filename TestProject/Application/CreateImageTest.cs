using Application.ImageApp;
using Application.Services.Contracts;
using Application.Validation;
using Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using System.Net;

namespace TestProject.Application
{
    public class CreateImageTest
    {
        private readonly Mock<IRepositoryImage> _repositoryImageMock;
        private readonly Mock<IFileService> _fileServiceMock;
        private readonly Mock<ILogger<CreateImage.Handler>> _loggerMock;
        private readonly CreateImage.Handler _handler;

        public CreateImageTest()
        {
            _repositoryImageMock = new Mock<IRepositoryImage>();
            _fileServiceMock = new Mock<IFileService>();
            _loggerMock = new Mock<ILogger<CreateImage.Handler>>();
            _handler = new CreateImage.Handler(_repositoryImageMock.Object, _fileServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsOkResult_WhenCreationIsSuccessful()
        {
            
            // Arrange
            var query = new CreateImage.Query {
                Name = "Test Image",
                Description = "Test Description",
                ImageFile = new FormFile(null, 0, 0, "ImageFile", "test.jpg")
            };

            var image = new Image {
                Id = 1,
                Name = "Name",
                Description = "Description"
            };

            _fileServiceMock.Setup(f => f.ConvertFileToByteArray(It.IsAny<IFormFile>()))
                            .ReturnsAsync(new byte[] { 1, 2, 3 });

            _repositoryImageMock.Setup(r => r.CreateImage(It.IsAny<Image>()))
                                .ReturnsAsync(image);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            var response = Assert.IsType<Response>(okResult.Value) ;
            var returnValue = response.Result as Image;

            Assert.True(response.IsSuccess);
            Assert.Equal("OK", response.Code);
            Assert.Equal("Creación exitosa", response.Message);
            Assert.Equal(image.Name, returnValue.Name);
        }

        [Fact]
        public async Task Handle_ReturnsInternalServerErrorResult_WhenExceptionIsThrown()
        {
            // Arrange
            var query = new CreateImage.Query {
                Name = "Test Image",
                Description = "Test Description",
                ImageFile = new FormFile(null, 0, 0, "ImageFile", "test.jpg")
            };

            _fileServiceMock.Setup(f => f.ConvertFileToByteArray(It.IsAny<IFormFile>()))
                            .ReturnsAsync(new byte[] { 1, 2, 3 });

            _repositoryImageMock.Setup(r => r.CreateImage(It.IsAny<Image>()))
                                .ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, internalServerErrorResult.StatusCode);

            var response = Assert.IsType<Response>(internalServerErrorResult.Value);

            Assert.False(response.IsSuccess);
            Assert.Equal("ERROR", response.Code);
            Assert.Equal("Test exception", response.Message);
        }
    }
}
