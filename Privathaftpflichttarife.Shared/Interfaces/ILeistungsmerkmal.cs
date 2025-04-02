using Privathaftpflichttarife.Shared.Enums;

namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface ILeistungsmerkmal
    {
        string Bezeichnung { get; set; }

        LeistungsmerkmalTyp Typ { get; set; }

        bool Wert { get; set; }
    }
}
