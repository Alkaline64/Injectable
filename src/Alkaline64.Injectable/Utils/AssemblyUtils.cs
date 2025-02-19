using System.Reflection;
using System.Text.RegularExpressions;

namespace Alkaline64.Injectable.Utils;

internal class AssemblyUtils
{
    internal static IList<InjectableAttribute> FindInjectables<TMarker>()
    {
        return FindInjectables(typeof(TMarker).Assembly);
    }

    internal static IList<InjectableAttribute> FindInjectables(string[] masks)
    {
        var injectables = new List<InjectableAttribute>();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(MatchesMask(masks)))
            injectables.AddRange(FindInjectables(assembly));

        return injectables;
    }

    private static List<InjectableAttribute> FindInjectables(Assembly assembly)
    {
        var injectables = new List<InjectableAttribute>();

        foreach (var type in assembly.GetTypes())
            foreach (var injectable in type.GetCustomAttributes<InjectableAttribute>(true))
                injectables.Add(injectable.ForType(type));

        return injectables;
    }

    private static Func<Assembly, bool> MatchesMask(string[] masks)
    {
        return assembly => masks.Length == 0 || masks.Any(mask => Regex.IsMatch(assembly.FullName!, AsRegex(mask)));
    }

    private static string AsRegex(string value)
    {
        return Regex.Escape($"{value.Replace("\\*", ".*")}^$");
    }
}
