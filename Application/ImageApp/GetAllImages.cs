using Application.Validation;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.ImageApp
{
    public class GetAllImages
    {

        public class Query: IRequest<IActionResult> {

            public int? CategoryId { get; set; }
        
        }

        public class Handler : IRequestHandler<Query, IActionResult>
        {
            private readonly IRepositoryImage _repositoryImage;

            public Handler(IRepositoryImage repositoryImage) { 
               _repositoryImage = repositoryImage;
            }

            public async Task<IActionResult> Handle(Query request, CancellationToken cancellationToken)
            {

                Response responseGetAll = new ();

                try
                {

                  var images = await _repositoryImage.GetAllImages(request.CategoryId);

                    responseGetAll.IsSuccess = images.Any();
                    responseGetAll.Code = "OK";
                    responseGetAll.Message = "Consulta exitosa";

                    return Response.ObjectResult(HttpStatusCode.OK, responseGetAll.IsSuccess, responseGetAll.Message, responseGetAll.Code, images);
                }catch (Exception ex)
                {
                    responseGetAll.IsSuccess = false;
                    responseGetAll.Code = "ERROR";
                    responseGetAll.Message = ex.Message;

                    return Response.ObjectResult(HttpStatusCode.InternalServerError, responseGetAll.IsSuccess, responseGetAll.Message, responseGetAll.Code, null);

                }
            }
        }
    }
}
