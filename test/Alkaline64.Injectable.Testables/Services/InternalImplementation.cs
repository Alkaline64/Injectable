namespace Alkaline64.Injectable.Tests.Accessibility.Services;

[Injectable<IService>(Lifetime.Transient)]
internal class InternalImplementation : IService
{
    public Guid Guid { get; } = Guid.NewGuid();
}
