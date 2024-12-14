namespace Alkaline64.Injectable.Tests.TryAdd.Services;

[Injectable<ITryAddProvider>(Lifetime.Scoped, TryAdd = true)]
public class TryAddService2 : ITryAddProvider
{
}
