using System.ComponentModel.DataAnnotations;
using Privathaftpflichttarife.Shared.Enums;

namespace Privathaftpflichttarife.Core.Models
{
    public class Leistungsmerkmal
    {
        // Eindeutige ID für Datenbankpersistenz
        public Guid Id { get; set; } = Guid.NewGuid();

        // Typ des Leistungsmerkmals
        [Required(ErrorMessage = "Leistungsmerkmaltyp ist erforderlich")]
        public LeistungsmerkmalTyp Typ { get; set; }

        // Wert des Leistungsmerkmals (true = vorhanden, false = nicht vorhanden)
        public bool Wert { get; set; }

        // Optionale Beschreibung für erweiterte Informationen
        [StringLength(500)]
        public string Beschreibung { get; set; }

        // FK zu einem Tarif (optional, je nach Datenmodell)
        public Guid? TarifId { get; set; }

        // Standard-Konstruktor
        public Leistungsmerkmal() { }

        // Konstruktor mit Parametern
        public Leistungsmerkmal(LeistungsmerkmalTyp typ, bool wert)
        {
            Id = Guid.NewGuid();
            Typ = typ;
            Wert = wert;
        }

        // Erweiterter Konstruktor
        public Leistungsmerkmal(LeistungsmerkmalTyp typ, bool wert, string beschreibung)
        {
            Id = Guid.NewGuid();
            Typ = typ;
            Wert = wert;
            Beschreibung = beschreibung;
        }

        // Gleichheitsvergleich
        public override bool Equals(object obj)
        {
            if (obj is Leistungsmerkmal other)
            {
                return Typ == other.Typ && Wert == other.Wert;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Typ, Wert);
        }

        // Benutzerfreundliche Darstellung
        public override string ToString()
        {
            return $"{GetLeistungsmerkmalName(Typ)}: {(Wert ? "Ja" : "Nein")}";
        }

        // Hilfsmethode für die Namensauflösung
        private string GetLeistungsmerkmalName(LeistungsmerkmalTyp typ)
        {
            return typ switch
            {
                LeistungsmerkmalTyp.MitversicherungVonKindern => "Mitversicherung von Kindern",
                LeistungsmerkmalTyp.Schlüsselverlust => "Schlüsselverlust",
                LeistungsmerkmalTyp.Regressansprüche => "Regressansprüche",
                _ => typ.ToString()
            };
        }

        // Methode zur Überprüfung, ob dieses Leistungsmerkmal eine Verbesserung darstellt
        public bool IstVerbesserungFuer(Leistungsmerkmal anderesMerkmal)
        {
            if (anderesMerkmal == null || Typ != anderesMerkmal.Typ)
            {
                return true; // Wenn das andere Merkmal nicht existiert, ist dieses eine Verbesserung
            }

            return Wert && !anderesMerkmal.Wert; // Nur wenn dieses true und das andere false ist
        }

        // Factory-Methode für Leistungsmerkmale
        public static Leistungsmerkmal Create(LeistungsmerkmalTyp typ, bool wert)
        {
            return new Leistungsmerkmal(typ, wert);
        }

        // Methode zum Klonen eines Leistungsmerkmals
        public Leistungsmerkmal Clone()
        {
            return new Leistungsmerkmal
            {
                Typ = Typ,
                Wert = Wert,
                Beschreibung = Beschreibung
            };
        }
    }
}
