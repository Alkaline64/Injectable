using Alkaline64.Injectable.Utils;

namespace Alkaline64.Injectable;

/// <summary>
/// The InjectionContext provides a context for injection priorities to sync between assemblies.
/// </summary>
public class InjectionContext
{
    internal List<InjectableAttribute> Injectables { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the Injection Context.
    /// </summary>
    /// <returns>A new Injection Context.</returns>
    public static InjectionContext NewContext() => new();

    /// <summary>
    /// Adds all Injectables in the marker assembly to the Injection Context.
    /// </summary>
    /// <typeparam name="TMarker">A marker-type from the assembly to evaluate for instances of the <see cref="InjectableAttribute">Injectable</see> attribute.</typeparam>
    /// <returns>The Injection Context.</returns>
    public InjectionContext AddInjectables<TMarker>()
    {
        Injectables.AddRange(AssemblyUtils.FindInjectables<TMarker>());

        return this;
    }
}
