using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Alkaline64.Injectable;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all Injectables in the marker assembly to the service collection.
    /// </summary>
    /// <typeparam name="TMarker">A marker-type from the assembly to search for an instances of then <see cref="InjectableAttribute">Injectable</see> attribute.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection RegisterInjectables<TMarker>(this IServiceCollection services)
    {
        foreach (var type in typeof(TMarker).Assembly.GetTypes())
            RegisterInjectables(services, type);

        return services;
    }

    private static void RegisterInjectables(IServiceCollection services, Type type)
    {
        foreach (var injectable in type.GetCustomAttributes<InjectableAttribute>(true))
            RegisterInjectable(services, type, injectable);
    }

    private static void RegisterInjectable(IServiceCollection services, Type type, InjectableAttribute injectable)
    {
        var serviceType = GetServiceType(type, injectable);
        var descriptor = GetServiceDescriptor(type, injectable, serviceType);

        if (injectable.UseTry)
            services.TryAdd(descriptor);
        else
            services.Add(descriptor);
    }

    private static Type GetServiceType(Type type, InjectableAttribute injectable) =>
        injectable.ServiceType is not null
            ? injectable.ServiceType
            : type;

    private static ServiceDescriptor GetServiceDescriptor(Type type, InjectableAttribute injectable, Type serviceType) =>
        injectable.Key is not null
            ? new ServiceDescriptor(serviceType, injectable.Key, type, injectable.Lifetime.AsServiceLifetime())
            : new ServiceDescriptor(serviceType, type, injectable.Lifetime.AsServiceLifetime());

    private static ServiceLifetime AsServiceLifetime(this Lifetime value) => value switch
    {
        Lifetime.Singleton => ServiceLifetime.Singleton,
        Lifetime.Scoped => ServiceLifetime.Scoped,
        Lifetime.Transient => ServiceLifetime.Transient,
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };
}
