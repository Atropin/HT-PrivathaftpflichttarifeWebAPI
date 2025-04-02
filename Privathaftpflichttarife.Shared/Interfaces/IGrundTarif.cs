namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface IGrundTarif : ITarif
    {
        decimal Praemie { get; set; }
    }
}
