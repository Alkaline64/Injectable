using Alkaline64.Injectable.Tests.Shared;

namespace Alkaline64.Injectable.Tests.Context2.Services;

[Injectable<IPrioritizedProvider>(Lifetime.Scoped, Priority = 1)]
public class PrioritizedService4 : IPrioritizedProvider
{
    public int Priority => 1;
}
