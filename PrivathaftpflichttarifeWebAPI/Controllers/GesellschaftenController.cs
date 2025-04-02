using Microsoft.AspNetCore.Mvc;
using Privathaftpflichttarife.Core.Models;
using Privathaftpflichttarife.Infrastructure.Mock;
using Privathaftpflichttarife.Shared.DTOs;
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
        public async Task<IActionResult> Post([FromBody] GesellschaftRequest request)
        {
            if (request.Name == null || request.Name.Equals(string.Empty, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest(new { message = "Name darf nicht leer sein" });
            }

            var gesellschaft = new Gesellschaft(request.Name);
            var gesellschaftResult = await _repository.CreateGesellschaftAsync(gesellschaft);

            return Ok(gesellschaftResult);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gesellschaften = await _repository.GetAllGesellschaftenAsync();
            return Ok(gesellschaften);
        }
    }
}
