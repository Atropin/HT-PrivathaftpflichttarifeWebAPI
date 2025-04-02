using Privathaftpflichttarife.Shared.DTOs;

namespace Privathaftpflichttarife.Shared.Interfaces
{
    public interface IGesellschaftRepository
    {

        Task<IEnumerable<IGesellschaft>> GetAllGesellschaftenAsync();
        Task<IGesellschaft> GetGesellschaftByIdAsync(Guid id);
        Task<IGesellschaft> CreateGesellschaftAsync(IGesellschaft gesellschaft);
        Task<IGesellschaft> UpdateGesellschaftAsync(Guid id, IGesellschaft gesellschaft);
        Task<bool> DeleteGesellschaftAsync(Guid id);


    }
}