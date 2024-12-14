namespace Alkaline64.Injectable.Tests.Keyed.Services;

[Injectable<IKeyedProvider>(Lifetime.Scoped, Key = nameof(KeyedService2))]
public class KeyedService2 : IKeyedProvider
{
    public string Key => nameof(KeyedService2);
}
