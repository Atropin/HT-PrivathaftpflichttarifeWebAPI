using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing;
using Privathaftpflichttarife.Infrastructure.Mock;
using Privathaftpflichttarife.Shared.Interfaces;

namespace PrivathaftpflichttarifeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GesellschaftenController : Controller
    {
        private readonly ITarifRepository _tarifRepository;
        public GesellschaftenController()
        {
            _tarifRepository = new InMemoryMock();
        }

        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {
            return Ok(new { message = $"Neue Gesellschaft mit Name {name}" });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gesellschaften = await _tarifRepository.GetAllGesellschaftenAsync();
            return Ok(gesellschaften);
        }
    }
}
