using Privathaftpflichttarife.Infrastructure.Repositories;
using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Infrastructure.Mock
{
    public static class MockData
    {
        private static List<IGesellschaft> _gesellschaften = MockGesellschaften.GetMockGesellschaften();
        private static InMemoryGesellschaftRepository _gesellschaftenRepository = null;
        private static InMemoryTarifRepository _tarifeRepository = null;

        public static InMemoryGesellschaftRepository GetMockGesellschaften()
        {
            if (_gesellschaftenRepository != null)
            {
                return _gesellschaftenRepository;
            }

            _gesellschaftenRepository = new InMemoryGesellschaftRepository(_gesellschaften);
            return _gesellschaftenRepository;
        }
        
        public static InMemoryTarifRepository GetMockTarife()
        {
            if (_gesellschaftenRepository == null)
            {
                GetMockGesellschaften();
            }

            if (_tarifeRepository != null)
            {
                return _tarifeRepository;
            }

            var g1 = MockTarife.GetMockGrundTarif1(_gesellschaften[0]);
            var g2 = MockTarife.GetMockGrundTarif2(_gesellschaften[1]);
            var b1 = MockTarife.GetMockBausteinTarif1(_gesellschaften[0]);
            var b2 = MockTarife.GetMockBausteinTarif2(_gesellschaften[1]);

            var l1 = new List<IGrundTarif> { g1, g2 };
            var l2 = new List<IBausteinTarif> { b1, b2 };

            _tarifeRepository = new InMemoryTarifRepository(l1, l2);    
            return _tarifeRepository;
        }
    }
}
