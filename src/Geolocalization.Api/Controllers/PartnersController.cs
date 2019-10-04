using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Geolocalization.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PartnersController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(id);
        }
    }
}