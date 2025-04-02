using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Privathaftpflichttarife.Core.Models;
using Privathaftpflichttarife.Shared.DTOs;
using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Infrastructure.Repositories
{
    public class InMemoryTarifRepository : ITarifRepository
    {
        private readonly List<IGrundTarif> _grundtarife;
        private readonly List<IBausteinTarif> _bausteintarife;

        public InMemoryTarifRepository()
        {
            _grundtarife = new List<IGrundTarif>();
            _bausteintarife = new List<IBausteinTarif>();
        }

        // Optional: Konstruktor für Mock-Daten
        public InMemoryTarifRepository(List<IGrundTarif> grundtarife, List<IBausteinTarif> bausteintarife)
        {
            _grundtarife = grundtarife ?? new List<IGrundTarif>();
            _bausteintarife = bausteintarife ?? new List<IBausteinTarif>();
        }

        public Task<IEnumerable<IGrundTarif>> GetAllGrundtarifeAsync()
        {
            return Task.FromResult(_grundtarife.AsEnumerable<IGrundTarif>());
        }

        public Task<IGrundTarif> GetGrundtarifByIdAsync(Guid id)
        {
            return Task.FromResult(_grundtarife.FirstOrDefault(g => g.Id == id));
        }

        public Task<IEnumerable<IGrundTarif>> GetGrundtarifeByGesellschaftAsync(string gesellschaftsName)
        {
            return Task.FromResult(_grundtarife.Where(g => g.Gesellschaft.Bezeichnung.Equals(gesellschaftsName, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<IEnumerable<IGrundTarif>> GetGueltigeGrundtarifeAsync(DateTime stichtag)
        {
            return Task.FromResult(_grundtarife.Where(g => g.GueltigkeitsDatum <= stichtag));
        }

        public Task<IGrundTarif> CreateGrundtarifAsync(IGrundTarif grundtarif)
        {
            if (grundtarif.Id == Guid.Empty)
            {
                grundtarif.Id = Guid.NewGuid();
            }

            _grundtarife.Add(grundtarif);
            return Task.FromResult(grundtarif);
        }

        public Task<IGrundTarif> UpdateGrundtarifAsync(Guid id, IGrundTarif grundtarif)
        {
            var existingTarif = _grundtarife.FirstOrDefault(g => g.Id == id);
            if (existingTarif == null)
            {
                return Task.FromResult<IGrundTarif>(null);
            }

            // Entferne den alten Tarif
            _grundtarife.Remove(existingTarif);

            // Stelle sicher, dass die ID beibehalten wird
            grundtarif.Id = id;

            // Füge den aktualisierten Tarif hinzu
            _grundtarife.Add(grundtarif);

            return Task.FromResult(grundtarif);
        }

        public Task<bool> DeleteGrundtarifAsync(Guid id)
        {
            var existingTarif = _grundtarife.FirstOrDefault(g => g.Id == id);
            if (existingTarif == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(_grundtarife.Remove(existingTarif));
        }

        public Task<IEnumerable<IBausteinTarif>> GetAllBausteintarifeAsync()
        {
            return Task.FromResult(_bausteintarife.AsEnumerable());
        }

        public Task<IBausteinTarif> GetBausteintarifByIdAsync(Guid id)
        {
            return Task.FromResult(_bausteintarife.FirstOrDefault(b => b.Id == id));
        }

        public Task<IEnumerable<IBausteinTarif>> GetBausteintarifeByGesellschaftAsync(string gesellschaftsName)
        {
            return Task.FromResult(_bausteintarife.Where(b => b.Gesellschaft.Bezeichnung.Equals(gesellschaftsName, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<IEnumerable<IBausteinTarif>> GetGueltigeBausteintarifeAsync(DateTime stichtag)
        {
            return Task.FromResult(_bausteintarife.Where(b => b.GueltigkeitsDatum <= stichtag));
        }

        public Task<IEnumerable<IBausteinTarif>> GetBausteintarifeForGrundtarifAsync(Guid grundtarifId)
        {
            var grundtarif = _grundtarife.FirstOrDefault(g => g.Id == grundtarifId);
            if (grundtarif == null)
            {
                return Task.FromResult(Enumerable.Empty<IBausteinTarif>());
            }

            // Finde passende Bausteintarife der gleichen Gesellschaft
            return Task.FromResult(_bausteintarife.Where(b =>
                b.Gesellschaft.Bezeichnung.Equals(grundtarif.Gesellschaft.Bezeichnung, StringComparison.OrdinalIgnoreCase)));
        }

        public Task<IBausteinTarif> CreateBausteintarifAsync(IBausteinTarif bausteintarif)
        {
            if (bausteintarif.Id == Guid.Empty)
            {
                bausteintarif.Id = Guid.NewGuid();
            }

            _bausteintarife.Add(bausteintarif);
            return Task.FromResult(bausteintarif);
        }

        public Task<IBausteinTarif> UpdateBausteintarifAsync(Guid id, IBausteinTarif bausteintarif)
        {
            var existingTarif = _bausteintarife.FirstOrDefault(b => b.Id == id);
            if (existingTarif == null)
            {
                return Task.FromResult<IBausteinTarif>(null);
            }

            // Entferne den alten Tarif
            _bausteintarife.Remove(existingTarif);

            // Stelle sicher, dass die ID beibehalten wird
            bausteintarif.Id = id;

            // Füge den aktualisierten Tarif hinzu
            _bausteintarife.Add(bausteintarif);

            return Task.FromResult(bausteintarif);
        }

        public Task<bool> DeleteBausteintarifAsync(Guid id)
        {
            var existingTarif = _bausteintarife.FirstOrDefault(b => b.Id == id);
            if (existingTarif == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(_bausteintarife.Remove(existingTarif));
        }

        public Task<IEnumerable<object>> GetAllTarifeAsync()
        {
            var alleTarife = new List<object>();
            alleTarife.AddRange(_grundtarife);
            alleTarife.AddRange(_bausteintarife);

            return Task.FromResult(alleTarife.AsEnumerable());
        }

        public Task<bool> TarifExistsAsync(Guid id)
        {
            return Task.FromResult(
                _grundtarife.Any(g => g.Id == id) ||
                _bausteintarife.Any(b => b.Id == id)
            );
        }

        public Task<bool> IsGrundTarifAsync(Guid id)
        {
            return Task.FromResult(_grundtarife.Any(g => g.Id == id));             
        }
    }
}
