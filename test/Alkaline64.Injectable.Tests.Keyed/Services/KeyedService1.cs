namespace Alkaline64.Injectable.Tests.Keyed.Services;

[Injectable<IKeyedProvider>(Lifetime.Scoped, Key = nameof(KeyedService1))]
public class KeyedService1 : IKeyedProvider
{
    public string Key => nameof(KeyedService1);
}
