using Alkaline64.Injectable;

namespace Comply.Drs.Shared.DependencyInjection.Tests.Services
{
    [Injectable(Lifetime.Scoped, UseTry = true)]
    public class ScopedService
    {
    }
}
