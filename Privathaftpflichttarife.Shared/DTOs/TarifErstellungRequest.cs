using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Privathaftpflichttarife.Shared.DTOs
{
    public class TarifErstellungRequest
    {
        public Guid Gesellschaft { get; set; }
        public bool IsBaustein { get; set; }
        public string Name { get; set; }
        public decimal Praemie { get; set; }
        public DateTime Gueltigkeit { get; set; }
    }
}
