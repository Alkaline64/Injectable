namespace Alkaline64.Injectable;

/// <summary>
/// Specifies the lifetime of the implementation's instance for a service.
/// </summary>
public enum Lifetime
{
    /// <summary>
    /// A single instance will be created.
    /// </summary>
    Singleton,

    /// <summary>
    /// A new instance will be created for each scope.
    /// </summary>
    Scoped,

    /// <summary>
    /// A new instance will be created each time the service is requested.
    /// </summary>
    Transient
}

/// <summary>
/// Register the decorated class as an injectable.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class InjectableAttribute : Attribute
{
    /// <summary>
    /// The type of the service which will be injectable.
    /// </summary>
    internal Type? ImplementationType { get; private set; } = null;

    /// <summary>
    /// The type of the service which will be injectable.
    /// </summary>
    public Type? ServiceType { get; init; } = null;

    /// <summary>
    /// Indicates the key used for registering a keyed service. Null indicates no key will be used.
    /// </summary>
    public string? Key { get; init; } = null;

    /// <summary>
    /// The priority in which to register this service in case there are multiple with the same Service Type.
    /// </summary>
    public int Priority { get; init; } = 0;

    /// <summary>
    /// Indicates whether to use TryAdd rather than Add to register the service.
    /// </summary>
    public bool AsTry { get; init; } = false;

    /// <summary>
    /// The lifetime of the service.
    /// </summary>
    public Lifetime Lifetime { get; } = Lifetime.Transient;

    /// <summary>
    /// Initializes a new instance of the InjectableAttribute.
    /// </summary>
    /// <param name="lifetime">The lifetime of the service.</param>
    public InjectableAttribute(Lifetime lifetime = Lifetime.Scoped)
    {
        Lifetime = lifetime;
    }

    internal InjectableAttribute ForType(Type type)
    {
        ImplementationType = type;
        return this;
    }
}

/// <summary>
/// Register the decorated class as an injectable for a specific service.
/// </summary>
/// <typeparam name="TServiceType">The service type for which to register the injectable</typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class InjectableAttribute<TServiceType> : InjectableAttribute
{
    /// <summary>
    /// Initializes a new instance of the InjectableAttribute.
    /// </summary>
    /// <param name="lifetime">The lifetime of the service.</param>
    public InjectableAttribute(Lifetime lifetime = Lifetime.Scoped) : base(lifetime)
    {
        ServiceType = typeof(TServiceType);
    }
}
