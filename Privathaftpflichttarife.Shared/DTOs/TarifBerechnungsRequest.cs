using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Privathaftpflichttarife.Shared.Enums;

namespace Privathaftpflichttarife.Shared.DTOs
{
    public class TarifBerechnungsRequest
    {
        public Guid GrundtarifId { get; set; }
        public DateTime Stichtag { get; set; } = DateTime.Now;
        public List<Guid> BausteintarifIds { get; set; } = new();
        public List<LeistungsmerkmalTyp> GeforderteMerkmale { get; set; } = new();
    }
}
