namespace Alkaline64.Injectable;

[AttributeUsage(AttributeTargets.Class)]
public  class InjectableOptionsAttribute : Attribute
{
    public string Key { get; }

    public string? Name { get; set; }

    /// <summary>
    /// The type of the service which will be injectable.
    /// </summary>
    public Type? ServiceType { get; protected set; } = null;

    public InjectableOptionsAttribute(string key)
    {
        Key = key;
    }
}
