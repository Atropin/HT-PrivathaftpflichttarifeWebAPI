using System.ComponentModel.DataAnnotations;

namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface IGesellschaft
    {
        [Required]
        Guid Id { get; set; }


        [Required]
        string Bezeichnung { get; set; }
    }
}
