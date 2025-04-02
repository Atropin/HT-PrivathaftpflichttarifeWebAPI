using Privathaftpflichttarife.Core.Models;
using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Infrastructure.Mock
{
    internal static class MockTarife
    {
        public static GrundTarif GetMockGrundTarif1(IGesellschaft gesellschaft)
        {
            return new GrundTarif("Rostbratwurst", gesellschaft)
            {
                Praemie = 100
            };
        }

        public static GrundTarif GetMockGrundTarif2(IGesellschaft gesellschaft)
        {
            return new GrundTarif("Thüringer Wurst", gesellschaft)
            {
                Praemie = 120
            };
        }

        public static BausteinTarif GetMockBausteinTarif1(IGesellschaft gesellschaft)
        {
            return new BausteinTarif("Mit Senf (Baustein)", gesellschaft)
            {
                Zusatzpraemie = 50
            };
        }
        public static BausteinTarif GetMockBausteinTarif2(IGesellschaft gesellschaft)
        {
            return new BausteinTarif("Mit Ketchup (Baustein)", gesellschaft)
            {
                Zusatzpraemie = 25
            };
        }
    }
}
