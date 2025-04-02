using Microsoft.AspNetCore.Mvc;
using Privathaftpflichttarife.Infrastructure.Mock;
using Privathaftpflichttarife.Infrastructure.Repositories;
using Privathaftpflichttarife.Shared.Interfaces;

namespace PrivathaftpflichttarifeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GesellschaftenController : Controller
    {
        private readonly IGesellschaftRepository _repository;
        public GesellschaftenController()
        {
            _repository = MockData.GetMockGesellschaften();
        }

        [HttpPost]
        public IActionResult Post([FromBody] string name)
        {
            return Ok(new { message = $"Neue Gesellschaft mit Name {name}" });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gesellschaften = await _repository.GetAllGesellschaftenAsync();
            return Ok(gesellschaften);
        }
    }
}
