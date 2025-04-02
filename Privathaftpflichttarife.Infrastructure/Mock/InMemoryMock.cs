using Privathaftpflichttarife.Core.Models;
using Privathaftpflichttarife.Infrastructure.Repositories;
using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Infrastructure.Mock
{
    public class InMemoryMock : ITarifRepository
    {
        private InMemoryTarifRepository _repo;

        public InMemoryMock()
        {
            _repo = new InMemoryTarifRepository();

            var g1 = new Gesellschaft("Nürnberger");

            _repo.CreateGrundtarifAsync(new GrundTarif("Rostbratwurst", g1)
            {
                Praemie = 100
            });
            _repo.CreateBausteintarifAsync(new BausteinTarif("Mit Senf (Baustein)", g1)
            {
                Zusatzpraemie = 50
            });

            var g2 = new Gesellschaft("Gothaer");

            _repo.CreateGrundtarifAsync(new GrundTarif("Thüringer Wurst", g2)
            {
                Praemie = 120
            });
            _repo.CreateBausteintarifAsync(new BausteinTarif("Mit Ketchup (Baustein)", g2)
            {
                Zusatzpraemie = 25
            });
        }

        public Task<IBausteinTarif> CreateBausteintarifAsync(IBausteinTarif bausteintarif)
        {
            return _repo.CreateBausteintarifAsync(bausteintarif);  
        }

        public Task<IGrundTarif> CreateGrundtarifAsync(IGrundTarif grundtarif)
        {
            return _repo.CreateGrundtarifAsync(grundtarif);
        }

        public Task<bool> DeleteBausteintarifAsync(Guid id)
        {
            return _repo.DeleteBausteintarifAsync(id);
        }

        public Task<bool> DeleteGrundtarifAsync(Guid id)
        {
            return _repo.DeleteGrundtarifAsync(id);
        }

        public Task<IEnumerable<IBausteinTarif>> GetAllBausteintarifeAsync()
        {
            return _repo.GetAllBausteintarifeAsync();
        }

        public Task<IEnumerable<IGrundTarif>> GetAllGrundtarifeAsync()
        {
            return _repo.GetAllGrundtarifeAsync();
        }

        public Task<IEnumerable<object>> GetAllTarifeAsync()
        {
            return _repo.GetAllTarifeAsync();
        }

        public Task<IBausteinTarif> GetBausteintarifByIdAsync(Guid id)
        {
            return _repo.GetBausteintarifByIdAsync(id); 
        }

        public Task<IEnumerable<IBausteinTarif>> GetBausteintarifeByGesellschaftAsync(string gesellschaftsName)
        {
            return _repo.GetBausteintarifeByGesellschaftAsync(gesellschaftsName);
        }

        public Task<IEnumerable<IBausteinTarif>> GetBausteintarifeForGrundtarifAsync(Guid grundtarifId)
        {
            return _repo.GetBausteintarifeForGrundtarifAsync(grundtarifId);
        }

        public Task<IGrundTarif> GetGrundtarifByIdAsync(Guid id)
        {
            return _repo.GetGrundtarifByIdAsync(id);
        }

        public Task<IEnumerable<IGrundTarif>> GetGrundtarifeByGesellschaftAsync(string gesellschaftsName)
        {
            return _repo.GetGrundtarifeByGesellschaftAsync(gesellschaftsName);
        }

        public Task<IEnumerable<IBausteinTarif>> GetGueltigeBausteintarifeAsync(DateTime stichtag)
        {
            return _repo.GetGueltigeBausteintarifeAsync(stichtag);
        }

        public Task<IEnumerable<IGrundTarif>> GetGueltigeGrundtarifeAsync(DateTime stichtag)
        {
            return _repo.GetGueltigeGrundtarifeAsync(stichtag);
        }

        public Task<bool> TarifExistsAsync(Guid id)
        {
            return _repo.TarifExistsAsync(id);
        }

        public Task<IBausteinTarif> UpdateBausteintarifAsync(Guid id, IBausteinTarif bausteintarif)
        {
            return _repo.UpdateBausteintarifAsync(id, bausteintarif);   
        }

        public Task<IGrundTarif> UpdateGrundtarifAsync(Guid id, IGrundTarif grundtarif)
        {
            return _repo.UpdateGrundtarifAsync(id, grundtarif);
        }

        public Task<IEnumerable<IGesellschaft>> GetAllGesellschaftenAsync()
        {
            return _repo.GetAllGesellschaftenAsync();  
        }
    }
}
