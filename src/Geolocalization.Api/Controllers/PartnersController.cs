using Geolocalization.Application.Command.Commands;
using Geolocalization.Application.Query.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Geolocalization.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PartnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreatePartnerCommand request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var partner = await _mediator.Send(request);

            return CreatedAtAction(nameof(GetById), new { id = partner.Id }, partner);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var partner = await _mediator.Send(new GetPartnerByIdQuery(id));

            return Ok(partner);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetByCoordinates([FromQuery]CoordinatesRequest coordinates)
        //{
        //    var partner = await _partnersRepository.GetByCoordinates(coordinates.Latitude, coordinates.Longitude);

        //    if (partner is null)
        //        return NotFound(default(object));

        //    return Ok(partner);
        //}
    }
}