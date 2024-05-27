using Application.Services.Contracts;
using Application.Validation;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using System.Net;

namespace Application.ImageApp
{
    public class CreateImage
    {

        public class Query: IRequest<IActionResult>
        {
            public string Name { get; set; }

            public string Description { get; set; }

            public IFormFile ImageFile { get; set; }
        }


        public class Handler : IRequestHandler<Query, IActionResult>
        {

            private readonly IRepositoryImage _repositoryImage;
            private readonly IFileService _fileService;
            private readonly ILogger _logger;


            public Handler(IRepositoryImage repositoryImage, IFileService fileService, ILogger<Handler> logger) { 
                _repositoryImage = repositoryImage;
                _fileService = fileService;
                _logger = logger;
            }   

            public async Task<IActionResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var imageArrayByte = await _fileService.ConvertFileToByteArray(request.ImageFile);

                Response responseCreate = new();
                Image image = new() {
                    Name = request.Name,
                    Description = request.Description,
                    ImageFile = imageArrayByte
                };


                try
                {
                    var response = await _repositoryImage.CreateImage(image);
                    responseCreate.IsSuccess = true;
                    responseCreate.Code = "OK";
                    responseCreate.Message = "Creación exitosa";

                    return Response.ObjectResult(HttpStatusCode.OK, responseCreate.IsSuccess, responseCreate.Message, responseCreate.Code, response);

                } catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    responseCreate.IsSuccess = false;
                    responseCreate.Code = "ERROR";

                    return Response.ObjectResult(HttpStatusCode.InternalServerError, responseCreate.IsSuccess, ex.Message, responseCreate.Code, null);
                }
            }
        }
    }
}
