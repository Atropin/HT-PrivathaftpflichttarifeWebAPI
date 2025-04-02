using Privathaftpflichttarife.Shared.DTOs;

namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface ITarifRepository
    {
        // Grundtarife
        Task<IEnumerable<IGrundTarif>> GetAllGrundtarifeAsync();
        Task<IGrundTarif> GetGrundtarifByIdAsync(Guid id);
        Task<IEnumerable<IGrundTarif>> GetGrundtarifeByGesellschaftAsync(string gesellschaftsName);
        Task<IEnumerable<IGrundTarif>> GetGueltigeGrundtarifeAsync(DateTime stichtag);
        Task<IGrundTarif> CreateGrundtarifAsync(IGrundTarif grundtarif);
        Task<IGrundTarif> UpdateGrundtarifAsync(Guid id, IGrundTarif grundtarif);
        Task<bool> DeleteGrundtarifAsync(Guid id);

        // Bausteintarife
        Task<IEnumerable<IBausteinTarif>> GetAllBausteintarifeAsync();
        Task<IBausteinTarif> GetBausteintarifByIdAsync(Guid id);
        Task<IEnumerable<IBausteinTarif>> GetBausteintarifeByGesellschaftAsync(string gesellschaftsName);
        Task<IEnumerable<IBausteinTarif>> GetGueltigeBausteintarifeAsync(DateTime stichtag);
        Task<IEnumerable<IBausteinTarif>> GetBausteintarifeForGrundtarifAsync(Guid grundtarifId);
        Task<IBausteinTarif> CreateBausteintarifAsync(IBausteinTarif bausteintarif);
        Task<IBausteinTarif> UpdateBausteintarifAsync(Guid id, IBausteinTarif bausteintarif);
        Task<bool> DeleteBausteintarifAsync(Guid id);

        // Allgemeine Tarif-Operationen
        Task<IEnumerable<object>> GetAllTarifeAsync(); // Gibt sowohl Grund- als auch Bausteintarife zurück
        Task<bool> TarifExistsAsync(Guid id);

        Task<IEnumerable<IGesellschaft>> GetAllGesellschaftenAsync();
    }
}
