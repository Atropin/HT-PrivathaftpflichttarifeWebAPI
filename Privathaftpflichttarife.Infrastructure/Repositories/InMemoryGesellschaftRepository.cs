using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Infrastructure.Repositories
{
    public class InMemoryGesellschaftRepository : IGesellschaftRepository
    {
        private readonly List<IGesellschaft> _gesellschaften;

        public InMemoryGesellschaftRepository()
        {
            _gesellschaften = new List<IGesellschaft>();
        }

        // Optional: Konstruktor für Mock-Daten
        public InMemoryGesellschaftRepository(List<IGesellschaft> gesellschaften)
        {
            _gesellschaften = gesellschaften ?? new List<IGesellschaft>();
        }

        public Task<IEnumerable<IGesellschaft>> GetAllGesellschaftenAsync()
        {
            return Task.FromResult(_gesellschaften.AsEnumerable<IGesellschaft>());
        }

        public Task<IGesellschaft> GetGesellschaftByIdAsync(Guid id)
        {
            return Task.FromResult(_gesellschaften.FirstOrDefault(g => g.Id == id));
        }

        public Task<IGesellschaft> CreateGesellschaftAsync(IGesellschaft gesellschaft)
        {
            if (gesellschaft.Id == Guid.Empty)
            {
                gesellschaft.Id = Guid.NewGuid();
            }

            _gesellschaften.Add(gesellschaft);
            return Task.FromResult(gesellschaft);
        }

        public Task<IGesellschaft> UpdateGesellschaftAsync(Guid id, IGesellschaft gesellschaft)
        {
            var existing = _gesellschaften.FirstOrDefault(g => g.Id == id);
            if (existing == null)
            {
                return Task.FromResult<IGesellschaft>(null);
            }

            // Entferne den alten Tarif
            _gesellschaften.Remove(existing);

            // Stelle sicher, dass die ID beibehalten wird
            gesellschaft.Id = id;

            // Füge den aktualisierten Tarif hinzu
            _gesellschaften.Add(gesellschaft);

            return Task.FromResult(gesellschaft);
        }

        public Task<bool> DeleteGesellschaftAsync(Guid id)
        {
            var existing = _gesellschaften.FirstOrDefault(g => g.Id == id);
            if (existing == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(_gesellschaften.Remove(existing));
        }

    }
}
