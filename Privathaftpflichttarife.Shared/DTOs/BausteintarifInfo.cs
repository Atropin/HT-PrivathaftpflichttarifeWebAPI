using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privathaftpflichttarife.Shared.DTOs
{

    public class BausteintarifInfo
    {
        public Guid Id { get; set; }
        public string Bezeichnung { get; set; }
        public decimal Praemie { get; set; }
        public List<LeistungsmerkmalInfo> Leistungsmerkmale { get; set; } = new();
    }
}
