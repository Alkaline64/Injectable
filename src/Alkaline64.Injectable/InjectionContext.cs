using Alkaline64.Injectable.Utils;

namespace Alkaline64.Injectable;

/// <summary>
/// The InjectionContext provides a context for injection priorities to sync between assemblies.
/// </summary>
public class InjectionContext
{
    internal List<InjectableAttribute> Injectables { get; set; } = [];

    public static InjectionContext NewContext() => new();

    public InjectionContext AddInjectables<TMarker>()
    {
        Injectables.AddRange(AssemblyUtils.FindInjectables<TMarker>());

        return this;
    }
}
