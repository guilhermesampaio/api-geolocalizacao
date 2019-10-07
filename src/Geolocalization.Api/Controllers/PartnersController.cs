using System.Net;
using System.Threading.Tasks;
using Geolocalization.Api.Entities;
using Geolocalization.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Geolocalization.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {
        private readonly IPartnersRepository _partnersRepository;

        public PartnersController(IPartnersRepository partnersRepository)
        {
            _partnersRepository = partnersRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var partner = await _partnersRepository.Get(id);
            return Ok(partner);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Partner partner)
        {
            try
            {
                await _partnersRepository.Create(partner);
                return CreatedAtAction(nameof(Get), new { id = partner.Id });
            }
            catch (System.Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }            
        }

        [HttpGet]
        public async Task<IActionResult> GetByCoordinates([FromQuery]Coordinates coordinates)
        {
            var partner = await _partnersRepository.GetByCoordinates(coordinates.Latitude, coordinates.Longitude);

            if (partner is null)
                return NotFound();

            return Ok(partner);
        }
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}