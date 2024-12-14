using System.Reflection;

namespace Alkaline64.Injectable.Utils;

internal class AssemblyUtils
{
    internal static IList<InjectableAttribute> FindInjectables<TMarker>()
    {
        var injectables = new List<InjectableAttribute>();

        foreach (var type in typeof(TMarker).Assembly.GetTypes())
            foreach (var injectable in type.GetCustomAttributes<InjectableAttribute>(true))
                injectables.Add(injectable.ForType(type));

        return injectables;
    }
}
