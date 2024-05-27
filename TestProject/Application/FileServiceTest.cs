using Application.Services.Service;
using Microsoft.AspNetCore.Http;

namespace TestProject.Application
{
    public class FileServiceTest
    {

        [Fact]
        public async Task ConvertFileToByteArray_ReturnByteArray()
        {
            var file = new FormFile(null, 0, 0, "ImageFile", "Test.jpg");

            var fileService = new FileService();

            var result = fileService.ConvertFileToByteArray(file);

            Assert.IsType<byte[]>(result.Result);
        }


        [Fact]
        public async Task ConvertFileToByteArray_ReturnEmptyByteArray()
        {
            var fileService = new FileService();

            var result = fileService.ConvertFileToByteArray(null);

            Assert.IsType<byte[]>(result.Result);

            Assert.Empty(result.Result);
        }
    }
}
