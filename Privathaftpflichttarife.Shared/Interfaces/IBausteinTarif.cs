namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface IBausteinTarif : ITarif
    {
        decimal Zusatzpraemie { get; set; }
    }
}
