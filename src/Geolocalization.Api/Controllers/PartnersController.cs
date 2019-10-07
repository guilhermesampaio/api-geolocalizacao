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
        public async Task Create([FromBody]Partner partner)
        {
            await _partnersRepository.Create(partner);

            CreatedAtAction(nameof(Get), new { id = partner.Id });
        }
    }
}