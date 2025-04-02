using Privathaftpflichttarife.Core.Models;
using Privathaftpflichttarife.Shared.Interfaces;

namespace Privathaftpflichttarife.Infrastructure.Mock
{
    internal static class MockGesellschaften
    {
        public static List<IGesellschaft> GetMockGesellschaften() => [
                new Gesellschaft("Nürnberger"),
                new Gesellschaft("Gothaer"),
            ];
    }
}
