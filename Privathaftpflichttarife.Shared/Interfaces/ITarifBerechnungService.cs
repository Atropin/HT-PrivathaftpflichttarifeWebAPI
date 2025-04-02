using Privathaftpflichttarife.Shared.DTOs;
using Privathaftpflichttarife.Shared.Enums;

namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface ITarifBerechnungService
    {
        Task<TarifBerechnungsResponse> BerechneGesamttarifAsync(TarifBerechnungsRequest request);
        Task<List<TarifBerechnungsResponse>> FindePassendeTarifeAsync(List<LeistungsmerkmalTyp> geforderteMerkmale);
    }
}
