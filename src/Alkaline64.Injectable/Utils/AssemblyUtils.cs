using System.Reflection;

namespace Alkaline64.Injectable.Utils;

internal class AssemblyUtils
{
    internal static (IList<InjectableAttribute>, IList<InjectableOptionsAttribute>) FindInjectables<TMarker>()
    {
        var injectables = new List<InjectableAttribute>();
        var injectableOptions = new List<InjectableOptionsAttribute>();

        foreach (var type in typeof(TMarker).Assembly.GetTypes())
        {
            foreach (var injectable in type.GetCustomAttributes<InjectableAttribute>(true))
                injectables.Add(injectable.ForType(type));

            foreach (var injectable in type.GetCustomAttributes<InjectableOptionsAttribute>(true))
                injectableOptions.Add(injectable);
        }

        return (injectables, injectableOptions);
    }
}
