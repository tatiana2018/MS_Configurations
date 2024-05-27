using Application.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Service
{
    public class FileService : IFileService
    {
        public async Task<byte[]> ConvertFileToByteArray(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Array.Empty<byte>();

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
