using Alkaline64.Injectable.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Alkaline64.Injectable.Extensions;

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
        services.RegisterInjectables(AssemblyUtils.FindInjectables<TMarker>());

        return services;
    }

    /// <summary>
    /// Registers all Injectables in the marker assembly to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="context">The injection context.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection RegisterInjectables(this IServiceCollection services, InjectionContext context)
    {
        services.RegisterInjectables(context.Injectables);

        return services;
    }

    private static void RegisterInjectables(this IServiceCollection services, IEnumerable<InjectableAttribute> injectables)
    {
        foreach (var injectable in injectables.OrderBy(x => x.ServiceType).ThenBy(x => x.Priority))
            services.RegisterInjectable(injectable);
    }

    private static void RegisterInjectable(this IServiceCollection services, InjectableAttribute injectable)
    {
        var descriptor = injectable.GetServiceDescriptor();

        if (injectable.TryAdd)
            services.TryAdd(descriptor);
        else
            services.Add(descriptor);
    }

    private static ServiceDescriptor GetServiceDescriptor(this InjectableAttribute injectable)
    {
        if (injectable.ImplementationType is null)
            throw new InvalidOperationException($"{nameof(injectable.ImplementationType)} has not been declared.");

        var serviceType = injectable.ServiceType ?? injectable.ImplementationType;
        if (!serviceType.IsAssignableFrom(injectable.ImplementationType))
            throw new InvalidOperationException($"{serviceType.FullName} is not assignable from {injectable.ImplementationType.FullName}.");

        return injectable.Key is not null
            ? new ServiceDescriptor(serviceType, injectable.Key, injectable.ImplementationType, injectable.Lifetime.AsServiceLifetime())
            : new ServiceDescriptor(serviceType, injectable.ImplementationType, injectable.Lifetime.AsServiceLifetime());
    }

    private static ServiceLifetime AsServiceLifetime(this Lifetime value) => value switch
    {
        Lifetime.Singleton => ServiceLifetime.Singleton,
        Lifetime.Scoped => ServiceLifetime.Scoped,
        Lifetime.Transient => ServiceLifetime.Transient,
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };
}
