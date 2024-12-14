namespace Alkaline64.Injectable.Tests.Lifetime.Services;

public class GuidProvider
{
    public Guid Guid { get; } = Guid.NewGuid();
}
