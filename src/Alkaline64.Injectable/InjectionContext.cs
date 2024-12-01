namespace Alkaline64.Injectable
{
    public class InjectionContext
    {
        public static InjectionContext Main { get; private set; } = new InjectionContext();
        
        internal List<InjectableAttribute> Injectables { get; set; } = [];

        public InjectionContext AddInjectables<TMarker>()
        {
            Injectables.AddRange(AssemblyUtils.FindInjectables<TMarker>());

            return this;
        }
    }
}
