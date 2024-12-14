using Alkaline64.Injectable.Tests.Shared;

namespace Alkaline64.Injectable.Tests.Context1.Services;

[Injectable<IPrioritizedProvider>(Lifetime.Scoped, Priority = 2)]
public class PrioritizedService1 : IPrioritizedProvider
{
    public int Priority => 2;
}
