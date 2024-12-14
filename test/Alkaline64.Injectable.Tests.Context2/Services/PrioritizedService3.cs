using Alkaline64.Injectable.Tests.Shared;

namespace Alkaline64.Injectable.Tests.Context2.Services;

[Injectable<IPrioritizedProvider>(Lifetime.Scoped, Priority = 3)]
public class PrioritizedService3 : IPrioritizedProvider
{
    public int Priority => 3;
}
