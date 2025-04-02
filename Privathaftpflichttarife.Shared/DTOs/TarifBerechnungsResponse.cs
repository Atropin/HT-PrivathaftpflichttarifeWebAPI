using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privathaftpflichttarife.Shared.DTOs
{
    public class TarifBerechnungsResponse
    {
        public string GesellschaftBezeichnung { get; set; }
        public string TarifBezeichnung { get; set; }
        public object Tarifbezeichnung { get; set; }
        public decimal GesamtPraemie { get; set; }
        public decimal Gesamtpraemie { get; set; }
        public List<LeistungsmerkmalInfo> Leistungsmerkmale { get; set; } = new();
        public List<BausteintarifInfo> Bausteintarife { get; set; } = new();
        public object GrundtarifId { get; set; }
        public object Gesellschaft { get; set; }
    }
}
