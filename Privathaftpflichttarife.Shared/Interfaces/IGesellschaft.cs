using System.ComponentModel.DataAnnotations;

namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface IGesellschaft
    {
        [Required]
        string Bezeichnung { get; set; }
    }
}
