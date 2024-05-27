using Application.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ms_configuration.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status502BadGateway)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public class BaseController : Controller
    {
        private IMediator _mediator;

        public IMediator Mediator { 
            get { return _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>()); }
            set { _mediator = value; }
        }
    }
}
