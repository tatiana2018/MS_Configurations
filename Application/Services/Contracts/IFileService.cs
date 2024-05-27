using Microsoft.AspNetCore.Http;

namespace Application.Services.Contracts
{
    public interface IFileService
    {
        Task<byte[]> ConvertFileToByteArray(IFormFile file);

    }
}
