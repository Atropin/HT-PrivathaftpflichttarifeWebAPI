﻿using System.ComponentModel.DataAnnotations;
using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Core.Models
{
    public class BausteinTarif : IBausteinTarif
    {
        public BausteinTarif(string bezeichnung, IGesellschaft gesellschaft)
        {
            Bezeichnung = bezeichnung;
            Gesellschaft = gesellschaft;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Bausteintarifbezeichnung ist erforderlich")]
        [StringLength(100, MinimumLength = 2)]
        public string Bezeichnung { get; set; }

        public DateTime GueltigkeitsDatum { get; set; } = DateTime.Now;

        [Range(0, 10000, ErrorMessage = "Prämie muss zwischen 0 und 10000 liegen")]
        public decimal Zusatzpraemie { get; set; }

        [Required(ErrorMessage = "Gesellschaft ist erforderlich")]
        public IGesellschaft Gesellschaft { get; set; }

        public List<ILeistungsmerkmal> Leistungsmerkmale { get; set; } = new();

        // Zusätzliche Validierungsmethode
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Bezeichnung)
                   && Zusatzpraemie >= 0
                   && Gesellschaft != null;
        }
    }
}

