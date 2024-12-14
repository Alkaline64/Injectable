using Alkaline64.Injectable.Tests.Shared;

namespace Alkaline64.Injectable.Tests.Context1.Services;

[Injectable<IPrioritizedProvider>(Lifetime.Scoped, Priority = 0)]
public class PrioritizedService2 : IPrioritizedProvider
{
    public int Priority => 0;
}
