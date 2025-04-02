namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface ITarif
    {
        Guid Id { get; set; }

        string Bezeichnung { get; set; }

        DateTime GueltigkeitsDatum { get; set; }

        public IGesellschaft Gesellschaft { get; set; }

        List<ILeistungsmerkmal> Leistungsmerkmale { get; set; }
    }
}