using System.ComponentModel.DataAnnotations;
using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Core.Models
{
    public class Gesellschaft : IGesellschaft
    {
        public Gesellschaft(string bezeichnung)
        {
            Id = Guid.NewGuid();
            Bezeichnung = bezeichnung;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Gesellschaftsbezeichnung ist erforderlich")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Bezeichnung muss zwischen 2 und 100 Zeichen lang sein")]
        public string Bezeichnung { get; set; }

        public List<IGrundTarif> Grundtarife { get; set; } = new();
        public List<IBausteinTarif> Bausteintarife { get; set; } = new();
    }
}
