using Privathaftpflichttarife.Shared.Interfaces;
using Privathaftpflichttarife.Core.Models;
using Privathaftpflichttarife.Shared.DTOs;
using Privathaftpflichttarife.Shared.Enums;

namespace Privathaftpflichttarife.Core.Services
{
    public class TarifBerechnungService : ITarifBerechnungService
    {
        private readonly ITarifRepository _tarifRepository;

        public TarifBerechnungService(ITarifRepository tarifRepository)
        {
            _tarifRepository = tarifRepository;
        }

        public async Task<TarifBerechnungsResponse> BerechneGesamttarifAsync(TarifBerechnungsRequest request)
        {
            // Grundtarif abrufen
            var grundtarif = await _tarifRepository.GetGrundtarifByIdAsync(request.GrundtarifId);
            if (grundtarif == null)
            {
                throw new ArgumentException("Grundtarif nicht gefunden", nameof(request.GrundtarifId));
            }

            // Bausteintarife abrufen
            var bausteintarifeTask = request.BausteintarifIds.Select(id =>
                _tarifRepository.GetBausteintarifByIdAsync(id)).ToList();
            await Task.WhenAll(bausteintarifeTask);
            var bausteintarife = bausteintarifeTask.Select(t => t.Result).ToList();

            // Validierung: Bausteintarife müssen zur selben Gesellschaft gehören
            if (bausteintarife.Any(bt => bt.Gesellschaft != grundtarif.Gesellschaft))
            {
                throw new InvalidOperationException("Bausteintarife müssen zur selben Gesellschaft wie der Grundtarif gehören");
            }

            // Gesamtprämie berechnen
            decimal gesamtPraemie = grundtarif.Praemie + bausteintarife.Sum(bt => bt.Zusatzpraemie);

            // Leistungsmerkmale zusammenstellen
            var alleMerkmale = new Dictionary<LeistungsmerkmalTyp, bool>();

            // Grundtarif-Merkmale hinzufügen
            foreach (var merkmal in grundtarif.Leistungsmerkmale)
            {
                alleMerkmale[merkmal.Typ] = merkmal.Wert;
            }

            // Bausteintarif-Merkmale hinzufügen (überschreiben Grundtarif-Merkmale)
            foreach (var bausteintarif in bausteintarife)
            {
                foreach (var merkmal in bausteintarif.Leistungsmerkmale)
                {
                    // Ein Bausteintarif kann ein Merkmal nur verbessern (true), nicht verschlechtern (false)
                    if (alleMerkmale.ContainsKey(merkmal.Typ))
                    {
                        alleMerkmale[merkmal.Typ] = alleMerkmale[merkmal.Typ] || merkmal.Wert;
                    }
                    else
                    {
                        alleMerkmale[merkmal.Typ] = merkmal.Wert;
                    }
                }
            }

            // Antwort zusammenstellen
            var response = new TarifBerechnungsResponse
            {
                GesellschaftBezeichnung = grundtarif.Gesellschaft.Bezeichnung,
                TarifBezeichnung = grundtarif.Bezeichnung,
                GesamtPraemie = gesamtPraemie,
                Leistungsmerkmale = alleMerkmale.Select(kv => new LeistungsmerkmalInfo
                {
                    Typ = kv.Key,
                    Wert = kv.Value,
                    Beschreibung = GetLeistungsmerkmalBeschreibung(kv.Key)
                }).ToList(),
                Bausteintarife = bausteintarife.Select(bt => new BausteintarifInfo
                {
                    Bezeichnung = bt.Bezeichnung,
                    Praemie = bt.Zusatzpraemie,
                    Leistungsmerkmale = bt.Leistungsmerkmale.Select(lm => new LeistungsmerkmalInfo
                    {
                        Typ = lm.Typ,
                        Wert = lm.Wert,
                        Beschreibung = GetLeistungsmerkmalBeschreibung(lm.Typ)
                    }).ToList()
                }).ToList()
            };

            return response;
        }

        public async Task<List<TarifBerechnungsResponse>> FindePassendeTarifeAsync(List<LeistungsmerkmalTyp> geforderteMerkmale)
        {
            // Alle Grundtarife abrufen
            var alleGrundtarife = await _tarifRepository.GetAllGrundtarifeAsync();

            // Alle Bausteintarife abrufen
            var alleBausteintarife = await _tarifRepository.GetAllBausteintarifeAsync();

            var ergebnisse = new List<TarifBerechnungsResponse>();

            // Jeden Grundtarif prüfen
            foreach (var grundtarif in alleGrundtarife)
            {
                // Passende Bausteintarife für diesen Grundtarif ermitteln
                var passendeBausteintarife = alleBausteintarife
                    .Where(bt => bt.Gesellschaft == grundtarif.Gesellschaft)
                    .ToList();

                // Alle möglichen Kombinationen der Bausteintarife durchgehen
                // Beginnend mit keinem Bausteintarif und dann alle möglichen Kombinationen
                var kombinationen = GeneriereBausteintarifKombinationen(passendeBausteintarife);

                foreach (var kombination in kombinationen)
                {
                    // Prüfen ob diese Kombination alle geforderten Merkmale erfüllt
                    if (PruefeMerkmaleErfuellung(grundtarif, kombination, geforderteMerkmale))
                    {
                        // Berechnung für diese Kombination durchführen
                        var request = new TarifBerechnungsRequest
                        {
                            GrundtarifId = grundtarif.Id,
                            BausteintarifIds = kombination.Select(bt => bt.Id).ToList()
                        };

                        var ergebnis = await BerechneGesamttarifAsync(request);
                        ergebnisse.Add(ergebnis);
                    }
                }
            }

            return ergebnisse;
        }

        // Prüft, ob der Grundtarif und die Bausteintarife zusammen alle geforderten Merkmale erfüllen
        private bool PruefeMerkmaleErfuellung(IGrundTarif grundtarif, List<IBausteinTarif> bausteintarife,
            List<LeistungsmerkmalTyp> geforderteMerkmale)
        {
            // Sammle alle Leistungsmerkmale des Grundtarifs
            var vorhandeneMerkmale = new Dictionary<LeistungsmerkmalTyp, bool>();

            foreach (var merkmal in grundtarif.Leistungsmerkmale)
            {
                vorhandeneMerkmale[merkmal.Typ] = merkmal.Wert;
            }

            // Füge Leistungsmerkmale der Bausteintarife hinzu
            foreach (var bausteintarif in bausteintarife)
            {
                foreach (var merkmal in bausteintarif.Leistungsmerkmale)
                {
                    if (vorhandeneMerkmale.ContainsKey(merkmal.Typ))
                    {
                        vorhandeneMerkmale[merkmal.Typ] = vorhandeneMerkmale[merkmal.Typ] || merkmal.Wert;
                    }
                    else
                    {
                        vorhandeneMerkmale[merkmal.Typ] = merkmal.Wert;
                    }
                }
            }

            // Prüfe, ob alle geforderten Merkmale mit "true" enthalten sind
            return geforderteMerkmale.All(typ =>
                vorhandeneMerkmale.ContainsKey(typ) && vorhandeneMerkmale[typ]);
        }

        // Generiert alle möglichen Kombinationen der Bausteintarife
        private List<List<IBausteinTarif>> GeneriereBausteintarifKombinationen(List<IBausteinTarif> bausteintarife)
        {
            var result = new List<List<IBausteinTarif>>();

            // Leere Kombination (kein Bausteintarif)
            result.Add(new List<IBausteinTarif>());

            // Alle möglichen Kombinationen
            for (int i = 0; i < bausteintarife.Count; i++)
            {
                int count = result.Count;
                for (int j = 0; j < count; j++)
                {
                    var current = new List<IBausteinTarif>(result[j]);
                    current.Add(bausteintarife[i]);
                    result.Add(current);
                }
            }

            return result;
        }

        // Hilfsmethode für Beschreibungen der Leistungsmerkmale
        private string GetLeistungsmerkmalBeschreibung(LeistungsmerkmalTyp typ)
        {
            return typ switch
            {
                LeistungsmerkmalTyp.MitversicherungVonKindern => "Mitversicherung von Kindern",
                LeistungsmerkmalTyp.Schlüsselverlust => "Schlüsselverlust",
                LeistungsmerkmalTyp.Regressansprüche => "Regressansprüche",
                _ => typ.ToString()
            };
        }
    }
}
