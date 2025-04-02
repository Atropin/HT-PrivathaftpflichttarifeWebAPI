using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Privathaftpflichttarife.Shared.Enums;

namespace Privathaftpflichttarife.Shared.DTOs
{
    public class LeistungsmerkmalInfo
    {
        public LeistungsmerkmalTyp Typ { get; set; }
        public bool Wert { get; set; }
        public string Beschreibung { get; set; }
    }
}
