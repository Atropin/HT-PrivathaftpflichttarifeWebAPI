using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Privathaftpflichttarife.Core.Models;
using Privathaftpflichttarife.Infrastructure.Mock;
using Privathaftpflichttarife.Infrastructure.Repositories;
using Privathaftpflichttarife.Shared.Interfaces;
using Privathaftpflichttarife.Shared.DTOs;

namespace PrivathaftpflichttarifeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifeController : Controller
    {
        private readonly IGesellschaftRepository _gesellschaftRepository;
        private readonly ITarifRepository _tarifRepository;
        public TarifeController()
        {
            _gesellschaftRepository = MockData.GetMockGesellschaften();
            _tarifRepository = MockData.GetMockTarife();
        }

        [HttpPost]
        public IActionResult Post([FromBody] TarifErstellungRequest request)
        {
            var gesellschaften = _gesellschaftRepository.GetAllGesellschaftenAsync().Result.ToList();
            var gesellschaft = gesellschaften.FirstOrDefault(g => g.Id == request.Gesellschaft);
            if (gesellschaft == default)
            { 
                return BadRequest(new { message = "Gesellschaft nicht gefunden" }); 
            }

            ITarif tarif = request.IsBaustein
                ? new BausteinTarif(request.Name, gesellschaft)
                : new GrundTarif(request.Name, gesellschaft);

            if (tarif is BausteinTarif bausteinTarif)
            {
                bausteinTarif.Zusatzpraemie = request.Praemie;
                bausteinTarif.GueltigkeitsDatum = request.Gueltigkeit;
                _tarifRepository.CreateBausteintarifAsync(bausteinTarif);
            }
            else if (tarif is GrundTarif grundTarif)
            {
                grundTarif.Praemie = request.Praemie;
                grundTarif.GueltigkeitsDatum = request.Gueltigkeit;
                _tarifRepository.CreateGrundtarifAsync(grundTarif);
            }
            else
            {
                return BadRequest(new { message = "Tarif konnte nicht erstellt werden" });
            }

            return Ok(tarif);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tarife = await _tarifRepository.GetAllTarifeAsync();
            return Ok(tarife);
        }
    }
}
