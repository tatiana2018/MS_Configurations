using Application.Validation;
using Data.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.ImageApp
{
    public class DeleteImage
    {

        public class Query: IRequest<IActionResult> {
        
          public int Id { get; set; }

        }


        public class Handler : IRequestHandler<Query, IActionResult>
        {
            private readonly IRepositoryImage _repositoryImage;

            public Handler (IRepositoryImage repositoryImage)
            {
                _repositoryImage = repositoryImage;
            }
            public async Task<IActionResult> Handle(Query request, CancellationToken cancellationToken)
            {


                Response responseDelete = new();


                try
                {
                    var result = await _repositoryImage.DeleteImage(request.Id);

                    if (result)
                    {
                        responseDelete.IsSuccess = true;
                        responseDelete.Code = "OK";
                        responseDelete.Message = "Eliminación exitosa";
                    } else
                    {
                        responseDelete.IsSuccess = false;
                        responseDelete.Code = "NOFOUND";
                        responseDelete.Message = "Registro no encontrado";
                    }

                    return Response.ObjectResult(HttpStatusCode.OK, responseDelete.IsSuccess, responseDelete.Message, responseDelete.Code, result);
                } catch (Exception ex)
                {
                    responseDelete.IsSuccess = false;
                    responseDelete.Code = "ERROR";
                    responseDelete.Message = ex.Message;
                    return Response.ObjectResult(HttpStatusCode.InternalServerError, responseDelete.IsSuccess, responseDelete.Message, responseDelete.Code, null);

                }
            }
        }
    }
}
