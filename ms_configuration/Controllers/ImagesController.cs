using Application.ImageApp;
using Application.Validation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net;

namespace ms_configuration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : BaseController
    {
       
        public ImagesController(IMediator _mediator)
        {
            Mediator = _mediator;
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateImage")]
        [ProducesResponseType(typeof(Image), StatusCodes.Status200OK)]
        public async Task<IActionResult> PostImage([FromForm] CreateImage.Query data )
        {  
            if(data is null)
            {
                return BadRequest("Los datos de entrada no pueden ser nulos");
            }
                return await Mediator.Send(data);
        }

        [HttpGet("GetAllImages")]
        [ProducesResponseType(typeof (Image), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllImages([FromQuery] GetAllImages.Query id)
        {
            return await Mediator.Send(id);
        }

        [HttpPut("DeleteImages")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteImages([FromBody] DeleteImage.Query id)
        {
            return await Mediator.Send(id);
        }
    }
}
