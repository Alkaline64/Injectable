namespace Alkaline64.Injectable.Tests.Keyed.Services;

[Injectable<IKeyedProvider>(Lifetime.Scoped, Key = nameof(KeyedService3))]
public class KeyedService3 : IKeyedProvider
{
    public string Key => nameof(KeyedService3);
}
